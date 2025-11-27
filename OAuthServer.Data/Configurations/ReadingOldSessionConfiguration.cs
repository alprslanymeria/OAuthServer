using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;

namespace OAuthServer.Data.Configurations;

public class ReadingOldSessionConfiguration : BaseOldSessionConfiguration<ReadingOldSession>
{
    public override void Configure(EntityTypeBuilder<ReadingOldSession> builder)
    {
        base.Configure(builder);

        // ENTITY SPECIFIC AYARLAR BURADA YAPILABİLİR.

        // RELATIONS
        builder.HasMany(x => x.ReadingSessionRows)
            .WithOne(y => y.ReadingOldSession)
            .HasForeignKey(y => y.ReadingOldSessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}