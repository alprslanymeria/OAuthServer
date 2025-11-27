using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;

namespace OAuthServer.Data.Configurations;

public class WritingConfiguration : IEntityTypeConfiguration<Writing>
{
    public void Configure(EntityTypeBuilder<Writing> builder)
    {
        // RELATIONS
        builder.HasMany(x => x.WritingBooks)
            .WithOne(y => y.Writing)
            .HasForeignKey(y => y.WritingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.WritingOldSessions)
            .WithOne(y => y.Writing)
            .HasForeignKey(y => y.WritingId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}