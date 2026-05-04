namespace MiddlewareStaticFilesApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseExceptionHandler("/error.html");

        app.Use(async (context, next) =>
        {
            var logger = context.RequestServices
                .GetRequiredService<ILogger<Startup>>();

            logger.LogInformation(
                "Request started: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await next();

            logger.LogInformation(
                "Response completed: {StatusCode} for {Method} {Path}",
                context.Response.StatusCode,
                context.Request.Method,
                context.Request.Path);
        });

        app.Use(async (context, next) =>
        {
            // The Content Security Policy limits script and style loading to this app.
            context.Response.Headers.ContentSecurityPolicy =
                "default-src 'self'; script-src 'self'; style-src 'self'; object-src 'none'; base-uri 'self'; frame-ancestors 'none'";
            context.Response.Headers.XContentTypeOptions = "nosniff";

            await next();
        });

        // HTTPS redirection enforces secure access before serving static content.
        app.UseHttpsRedirection();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.MapGet("/throw", () =>
        {
            throw new InvalidOperationException("Demo exception for custom error middleware.");
        });

        app.MapGet("/health", () => Results.Ok("Middleware app is running."));
    }
}
