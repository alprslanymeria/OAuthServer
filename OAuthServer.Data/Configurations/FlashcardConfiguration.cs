using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data.Configurations
{
    public class FlashcardConfiguration : IEntityTypeConfiguration<Flashcard>
    {
        public void Configure(EntityTypeBuilder<Flashcard> builder)
        {
            // RELATIONS
            builder.HasMany(x => x.FlashcardCategories)
                .WithOne(y => y.Flashcard)
                .HasForeignKey(y => y.FlashcardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.FlashcardOldSessions)
                .WithOne(y => y.Flashcard)
                .HasForeignKey(y => y.FlashcardId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
