using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAuthServer.Core.Models;

namespace OAuthServer.Data.Configurations;

public class FlashcardCategoryConfiguration : IEntityTypeConfiguration<FlashcardCategory>
{
    public void Configure(EntityTypeBuilder<FlashcardCategory> builder)
    {
        // RELATIONS
        builder.HasMany(x => x.FlashcardOldSessions)
            .WithOne(y => y.FlashcardCategory)
            .HasForeignKey(y => y.FlashcardCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.DeckWords)
            .WithOne(y => y.FlashcardCategory)
            .HasForeignKey(y => y.FlashcardCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}