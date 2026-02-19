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
        // BU ASSEMBLY İÇERİSİNDE "IEntityTypeConfiguration" INTERFACE'İNİ IMPLEMENT EDEN TÜM CLASS'LARI OTOMATİK OLARAK BULUP UYGULAR
        // THIS ASSEMBLY AUTOMATICALLY FINDS AND APPLIES ALL CLASSES THAT IMPLEMENT THE "IEntityTypeConfiguration" INTERFACE WITHIN THIS ASSEMBLY
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}