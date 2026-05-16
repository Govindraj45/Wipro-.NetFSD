using OnlineBookstoreApp.Filters;
using OnlineBookstoreApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Books", "AdminOnly");
    options.Conventions.AllowAnonymousToPage("/Account/Login");
    options.Conventions.AllowAnonymousToPage("/Account/Register");
}).AddMvcOptions(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
    options.Filters.Add<RequestLogFilter>();
    options.Filters.Add<SessionCartFilter>();
});

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddAuthentication("BookstoreCookie")
    .AddCookie("BookstoreCookie", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

builder.Services.AddScoped<RequestLogFilter>();
builder.Services.AddScoped<SessionCartFilter>();
builder.Services.AddScoped<AdminInventoryFilter>();
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddScoped<CartSessionService>();
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "book-details",
    pattern: "catalog/{id:int}",
    defaults: new { controller = "Books", action = "Details" });

app.MapControllerRoute(
    name: "catalog",
    pattern: "catalog",
    defaults: new { controller = "Books", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
