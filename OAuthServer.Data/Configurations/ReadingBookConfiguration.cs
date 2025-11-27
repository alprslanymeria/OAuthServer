using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;

namespace OAuthServer.Data.Configurations;

public class ReadingBookConfiguration : IEntityTypeConfiguration<ReadingBook>
{
    public void Configure(EntityTypeBuilder<ReadingBook> builder)
    {
        builder.HasMany(x => x.ReadingOldSessions)
            .WithOne(y => y.ReadingBook)
            .HasForeignKey(y => y.ReadingBookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}