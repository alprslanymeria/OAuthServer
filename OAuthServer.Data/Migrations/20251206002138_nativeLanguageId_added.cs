using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuthServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class nativeLanguageId_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NativeLanguageId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NativeLanguageId",
                table: "AspNetUsers");
        }
    }
}
