using AdvancedRazorCatalog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Products/Details", "products/{id:int}");
    options.Conventions.AddPageRoute("/Products/ByCategory", "categories/{slug}");
});
builder.Services.AddSingleton<ProductStore>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
