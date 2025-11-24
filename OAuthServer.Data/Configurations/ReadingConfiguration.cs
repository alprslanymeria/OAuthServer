using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data.Configurations
{
    public class ReadingConfiguration : IEntityTypeConfiguration<Reading>
    {
        public void Configure(EntityTypeBuilder<Reading> builder)
        {
            builder.HasMany(x => x.ReadingBooks)
                .WithOne(y => y.Reading)
                .HasForeignKey(y => y.ReadingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ReadingOldSessions)
                .WithOne(y => y.Reading)
                .HasForeignKey(y => y.ReadingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
