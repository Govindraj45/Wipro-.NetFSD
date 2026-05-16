using BookstoreAdoNetApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<BookRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
