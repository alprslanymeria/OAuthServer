using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data.Configurations
{
    public class PracticeConfiguration : IEntityTypeConfiguration<Practice>
    {
        public void Configure(EntityTypeBuilder<Practice> builder)
        {
            // RELATIONS
            builder.HasMany(x => x.Flashcards)
                .WithOne(y => y.Practice)
                .HasForeignKey(y => y.PracticeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Listenings)
                .WithOne(y => y.Practice)
                .HasForeignKey(y => y.PracticeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Writings)
                .WithOne(y => y.Practice)
                .HasForeignKey(y => y.PracticeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Readings)
                .WithOne(y => y.Practice)
                .HasForeignKey(y => y.PracticeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
