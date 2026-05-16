using BookStoreApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<BookStoreRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
