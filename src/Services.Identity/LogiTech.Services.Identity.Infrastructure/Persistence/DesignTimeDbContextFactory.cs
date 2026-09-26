using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LogiTech.Services.Identity.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
        
        // هذا الـ Connection String يستخدم فقط أثناء عمل الـ Migrations
        // لتجنب أي مشاكل في قراءة ملف appsettings.json من خلال أدوات الـ EF Core
        var connectionString = "Host=db.vmwirdurhwbhzmulxjyb.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=Aya27353645$$$";
        
        optionsBuilder.UseNpgsql(connectionString);
        
        return new IdentityDbContext(optionsBuilder.Options);
    }
}