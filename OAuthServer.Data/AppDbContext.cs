using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OAuthServer.Core.Models;

namespace OAuthServer.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, IdentityRole, string>(options)
{

    // DB SETS
    public DbSet<UserRefreshToken> UserRefreshToken { get; set; }

    // OVERRIDE ON MODEL CREATING
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // APPLY CONFIGURATIONS
        // BU ASSEMBLY İÇERİSİNDE "IEntityTypeConfiguration" INTERFACE'İNİ IMPLEMENT EDEN TÜM CLASS'LARI OTOMATİK OLARAK BULUP UYGULAR
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}
