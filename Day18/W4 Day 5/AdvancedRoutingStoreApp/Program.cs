using AdvancedRoutingStoreApp.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["allowedcategory"] = typeof(AllowedCategoryRouteConstraint);
    options.ConstraintMap["pricerange"] = typeof(PriceRangeRouteConstraint);
    options.ConstraintMap["guidslug"] = typeof(GuidSlugRouteConstraint);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "product-details",
    pattern: "products/{category:allowedcategory}/{id:int}",
    defaults: new { controller = "Products", action = "Details" });

app.MapControllerRoute(
    name: "product-filter",
    pattern: "products/filter/{category:allowedcategory}/{priceRange:pricerange}",
    defaults: new { controller = "Products", action = "Filter" });

app.MapControllerRoute(
    name: "user-orders",
    pattern: "users/{username}/orders/{orderId:int?}",
    defaults: new { controller = "Users", action = "Orders" });

app.MapControllerRoute(
    name: "promo",
    pattern: "promo/{promoId:guidslug}",
    defaults: new { controller = "Home", action = "Promo" });

app.MapControllerRoute(
    name: "dashboard",
    pattern: "dashboard/{role}",
    defaults: new { controller = "Dashboard", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
