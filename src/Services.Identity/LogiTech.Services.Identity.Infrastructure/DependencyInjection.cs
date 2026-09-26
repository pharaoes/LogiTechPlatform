using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogiTech.Services.Identity.Infrastructure.Persistence;

namespace LogiTech.Services.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        
        // تسجيل DbContextOptions يدوياً
        services.AddScoped<DbContextOptions<IdentityDbContext>>(sp =>
        {
            var builder = new DbContextOptionsBuilder<IdentityDbContext>();
            builder.UseNpgsql(connectionString);
            return builder.Options;
        });
        
        // تسجيل IdentityDbContext يدوياً
        services.AddScoped<IdentityDbContext>(sp =>
        {
            var options = sp.GetRequiredService<DbContextOptions<IdentityDbContext>>();
            return new IdentityDbContext(options);
        });
        
        return services;using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogiTech.Services.Identity.Infrastructure.Persistence;

namespace LogiTech.Services.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        
        // ⚠️ هام جداً: اكتب <IdentityDbContext> بيدك هنا بعد AddDbContext
        // إذا وجدت أن المحرر مسحها بعد اللصق، امسح المسافة واكتبها يدوياً فوراً
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}
    }
}