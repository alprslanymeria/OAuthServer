using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, IdentityRole, string>(options)
    {

        // DB SETS
        public DbSet<DeckVideo> DeckVideo { get; set; }
        public DbSet<DeckWord> DeckWord { get; set; }
        public DbSet<Flashcard> Flashcard { get; set; }
        public DbSet<FlashcardCategory> FlashcardCategory { get; set; }
        public DbSet<FlashcardOldSession> FlashcardOldSession { get; set; }
        public DbSet<FlashcardSessionRow> FlashcardSessionRow { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Listening> Listening { get; set; }
        public DbSet<ListeningCategory> ListeningCategory { get; set; }
        public DbSet<ListeningOldSession> ListeningOldSession { get; set; }
        public DbSet<ListeningSessionRow> ListeningSessionRow { get; set; }
        public DbSet<Practice> Practice { get; set; }
        public DbSet<Reading> Reading { get; set; }
        public DbSet<ReadingBook> ReadingBook { get; set; }
        public DbSet<ReadingOldSession> ReadingOldSession { get; set; }
        public DbSet<ReadingSessionRow> ReadingSessionRow { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public DbSet<Writing> Writing { get; set; }
        public DbSet<WritingBook> WritingBook { get; set; }
        public DbSet<WritingOldSession> WritingOldSession { get; set; }
        public DbSet<WritingSessionRow> WritingSessionRow { get; set; }

        // OVERRIDE ON MODEL CREATING
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // APPLY CONFIGURATIONS
            // BU ASSEMBLY İÇERİSİNDE "IEntityTypeConfiguration" INTERFACE'İNİ IMPLEMENT EDEN TÜM CLASS'LARI OTOMATİK OLARAK BULUP UYGULAR
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
