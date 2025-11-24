using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            // RELATIONS
            builder.HasMany(x => x.Users)
                .WithOne(y => y.Language)
                .HasForeignKey(y => y.NativeLanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Practices)
                .WithOne(y => y.Language)
                .HasForeignKey(y => y.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Flashcards)
                .WithOne(y => y.Language)
                .HasForeignKey(y => y.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Listenings)
                .WithOne(y => y.Language)
                .HasForeignKey(y => y.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Writings)
                .WithOne(y => y.Language)
                .HasForeignKey(y => y.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Readings)
                .WithOne(y => y.Language)
                .HasForeignKey(y => y.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
