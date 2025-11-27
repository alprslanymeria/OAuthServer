using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;

namespace OAuthServer.Data.Configurations;

public class ListeningConfiguration : IEntityTypeConfiguration<Listening>
{
    public void Configure(EntityTypeBuilder<Listening> builder)
    {
        // RELATIONS
        builder.HasMany(x => x.ListeningCategories)
            .WithOne(y => y.Listening)
            .HasForeignKey(y => y.ListeningId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ListeningOldSessions)
            .WithOne(y => y.Listening)
            .HasForeignKey(y => y.ListeningId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}