using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data.Configurations
{
    public class ListeningOldSessionConfiguration : BaseOldSessionConfiguration<ListeningOldSession>
    {
        public override void Configure(EntityTypeBuilder<ListeningOldSession> builder)
        {
            base.Configure(builder);

            // ENTITY SPECIFIC AYARLAR BURADA YAPILABİLİR.

            // RELATIONS
            builder.HasMany(x => x.ListeningSessionRows)
                .WithOne(y => y.ListeningOldSession)
                .HasForeignKey(y => y.ListeningOldSessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
