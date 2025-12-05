using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuthServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class remove_native_language_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NativeLanguageId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NativeLanguageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                defaultValue: 2);
        }
    }
}
