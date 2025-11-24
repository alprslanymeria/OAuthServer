using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data.Configurations
{
    public class WritingOldSessionConfiguration : BaseOldSessionConfiguration<WritingOldSession>
    {
        public override void Configure(EntityTypeBuilder<WritingOldSession> builder)
        {
            base.Configure(builder);

            // ENTITY SPECIFIC AYARLAR BURADA YAPILABİLİR.

            // RELATIONS
            builder.HasMany(x => x.WritingSessionRows)
                .WithOne(y => y.WritingOldSession)
                .HasForeignKey(y => y.WritingOldSessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
