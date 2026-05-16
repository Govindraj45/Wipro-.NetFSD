using AdvancedMvcFiltersApp.Filters;
using AdvancedMvcFiltersApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalErrorFilter>();
    options.Filters.Add<AuditLoggingFilter>();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuditLoggingFilter>();
builder.Services.AddScoped<GlobalErrorFilter>();
builder.Services.AddScoped<SessionAuthenticationFilter>();
builder.Services.AddScoped<RoleAuthorizationFilter>();
builder.Services.AddSingleton<IUserContextService, DemoUserContextService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Index}/{id?}");

app.Run();
