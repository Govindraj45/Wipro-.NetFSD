using AdvancedRazorPagesApp.Services;

namespace AdvancedRazorPagesApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ProductStore>();

        services.AddRazorPages(options =>
        {
            // Custom route mappings demonstrate route parameters for product details and category filtering.
            options.Conventions.AddPageRoute("/Products/Details", "Products/{id:int}");
            options.Conventions.AddPageRoute("/Products/Category", "Products/Category/{categoryName}");
        });
    }

    public void Configure(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
    }
}
