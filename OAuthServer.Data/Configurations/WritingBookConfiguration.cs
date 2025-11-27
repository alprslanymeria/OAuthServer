using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;

namespace OAuthServer.Data.Configurations;

public class WritingBookConfiguration : IEntityTypeConfiguration<WritingBook>
{
    public void Configure(EntityTypeBuilder<WritingBook> builder)
    {
        // RELATIONS
        builder.HasMany(x => x.WritingOldSessions)
            .WithOne(y => y.WritingBook)
            .HasForeignKey(y => y.WritingBookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}