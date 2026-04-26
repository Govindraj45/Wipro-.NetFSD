using Microsoft.AspNetCore.StaticFiles;

namespace MiddlewareStaticFilesApp;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        if (!env.IsDevelopment())
        {
            app.UseHsts();
        }

        // Sends unexpected failures to a friendly static error page.
        app.UseExceptionHandler("/error.html");

        // Static files should be requested over HTTPS whenever the app is hosted with TLS.
        app.UseHttpsRedirection();

        // Adds a basic Content Security Policy and browser hardening headers for every response.
        app.Use(async (context, next) =>
        {
            context.Response.Headers.ContentSecurityPolicy =
                "default-src 'self'; script-src 'self'; style-src 'self'; img-src 'self' data:; object-src 'none'; base-uri 'self'; frame-ancestors 'none'";
            context.Response.Headers.XContentTypeOptions = "nosniff";
            context.Response.Headers.XFrameOptions = "DENY";
            context.Response.Headers["Referrer-Policy"] = "no-referrer";

            await next();
        });

        // Logs incoming request details and the final response status code.
        app.Use(async (context, next) =>
        {
            logger.LogInformation(
                "Incoming request: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await next();

            logger.LogInformation(
                "Outgoing response: {StatusCode} for {Method} {Path}",
                context.Response.StatusCode,
                context.Request.Method,
                context.Request.Path);
        });

        StaticFileOptions staticFileOptions = new()
        {
            ContentTypeProvider = new FileExtensionContentTypeProvider(),
            OnPrepareResponse = context =>
            {
                context.Context.Response.Headers.CacheControl = "public,max-age=3600";
                context.Context.Response.Headers.XContentTypeOptions = "nosniff";
            }
        };

        app.UseDefaultFiles();
        app.UseStaticFiles(staticFileOptions);

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/api/status", async context =>
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    Application = "Middleware Static Files App",
                    Status = "Running",
                    UtcTime = DateTime.UtcNow
                });
            });

            endpoints.MapGet("/throw", _ =>
            {
                throw new InvalidOperationException("Sample unhandled exception for middleware testing.");
            });
        });
    }
}
