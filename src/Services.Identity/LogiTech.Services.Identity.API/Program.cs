using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LogiTech.Services.Identity.Application.Authentication.Commands.Register;
using LogiTech.Services.Identity.Application.Common.Interfaces;
using LogiTech.Services.Identity.Infrastructure.Authentication;
using LogiTech.Services.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. إضافة الخدمات والـ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// 2. تسجيل خدمات الـ Infrastructure والـ Application
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<RegisterCommandHandler>();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// 3. تفعيل Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LogiTech Identity API v1");
    c.RoutePrefix = "swagger";
});

app.MapGet("/weatherforecast", () =>
{
    return new[] { "Warm", "Cool", "Hot" };
})
.WithName("GetWeatherForecast");

// >>> أضف هذا السطر هنا لتحويل الصفحة الرئيسية تلقائياً إلى السواجر <<<
app.MapGet("/", async context =>
{
    context.Response.Redirect("/swagger");
    await Task.CompletedTask;
});

app.MapControllers();

app.Run();
