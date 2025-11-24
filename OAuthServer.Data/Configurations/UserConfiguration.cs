using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.NativeLanguageId).HasDefaultValue(2);

            // RELATIONS
            builder.HasMany(x => x.Flashcards)
                .WithOne(y => y.User)
                .HasForeignKey(y => y.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Listenings)
                .WithOne(y => y.User)
                .HasForeignKey(y => y.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Writings)
                .WithOne(y => y.User)
                .HasForeignKey(y => y.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Readings)
                .WithOne(y => y.User)
                .HasForeignKey(y => y.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
