using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuthServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Language (name, imageUrl) VALUES
                ('english', '/images/flags/uk.png'),
                ('turkish', '/images/flags/tr.png'),
                ('german', '/images/flags/de.png'),
                ('russian', '/images/flags/rs.png');

                INSERT INTO Practice (languageId, name) VALUES
                (1, 'flashcard'),
                (1, 'reading'),
                (1, 'writing'),
                (1, 'listening'),
                (2, 'flashcard'),
                (2, 'reading'),
                (2, 'writing'),
                (2, 'listening'),
                (3, 'flashcard'),
                (3, 'reading'),
                (3, 'writing'),
                (3, 'listening'),
                (4, 'flashcard'),
                (4, 'reading'),
                (4, 'writing'),
                (4, 'listening');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM Practice WHERE languageId IN (1,2,3,4);
                DELETE FROM Language WHERE name IN ('english','turkish','german','russian');
            ");
        }
    }
}
