using DatabaseSecurityDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDataProtection();
builder.Services.AddSingleton<CredentialService>();
builder.Services.AddSingleton<CustomerSecurityRepository>();
builder.Services.AddSingleton<SecurityAssessmentService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
