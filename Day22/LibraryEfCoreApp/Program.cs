using LibraryEfCoreApp.Data;
using LibraryEfCoreApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseInMemoryDatabase("W5Day4LibraryDb"));
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    SeedData.Initialize(db);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
