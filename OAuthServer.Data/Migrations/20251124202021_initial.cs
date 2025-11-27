using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuthServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshToken", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NativeLanguageId = table.Column<int>(type: "int", nullable: true, defaultValue: 2),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Language_NativeLanguageId",
                        column: x => x.NativeLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Practice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Practice_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flashcard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    PracticeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flashcard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flashcard_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flashcard_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Flashcard_Practice_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Listening",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    PracticeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listening", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listening_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listening_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Listening_Practice_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reading",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    PracticeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reading", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reading_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reading_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reading_Practice_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Writing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    PracticeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Writing_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Writing_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Writing_Practice_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FlashcardCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlashcardId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashcardCategory_Flashcard_FlashcardId",
                        column: x => x.FlashcardId,
                        principalTable: "Flashcard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListeningCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListeningId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeningCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListeningCategory_Listening_ListeningId",
                        column: x => x.ListeningId,
                        principalTable: "Listening",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReadingBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReadingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeftColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingBook_Reading_ReadingId",
                        column: x => x.ReadingId,
                        principalTable: "Reading",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WritingBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WritingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeftColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WritingBook_Writing_WritingId",
                        column: x => x.WritingId,
                        principalTable: "Writing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeckWord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlashcardCategoryId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeckWord_FlashcardCategory_FlashcardCategoryId",
                        column: x => x.FlashcardCategoryId,
                        principalTable: "FlashcardCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlashcardOldSession",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlashcardId = table.Column<int>(type: "int", nullable: false),
                    FlashcardCategoryId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardOldSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashcardOldSession_FlashcardCategory_FlashcardCategoryId",
                        column: x => x.FlashcardCategoryId,
                        principalTable: "FlashcardCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlashcardOldSession_Flashcard_FlashcardId",
                        column: x => x.FlashcardId,
                        principalTable: "Flashcard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeckVideo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListeningCategoryId = table.Column<int>(type: "int", nullable: false),
                    Correct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeckVideo_ListeningCategory_ListeningCategoryId",
                        column: x => x.ListeningCategoryId,
                        principalTable: "ListeningCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListeningOldSession",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListeningId = table.Column<int>(type: "int", nullable: false),
                    ListeningCategoryId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeningOldSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListeningOldSession_ListeningCategory_ListeningCategoryId",
                        column: x => x.ListeningCategoryId,
                        principalTable: "ListeningCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListeningOldSession_Listening_ListeningId",
                        column: x => x.ListeningId,
                        principalTable: "Listening",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReadingOldSession",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReadingId = table.Column<int>(type: "int", nullable: false),
                    ReadingBookId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingOldSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingOldSession_ReadingBook_ReadingBookId",
                        column: x => x.ReadingBookId,
                        principalTable: "ReadingBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReadingOldSession_Reading_ReadingId",
                        column: x => x.ReadingId,
                        principalTable: "Reading",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WritingOldSession",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WritingId = table.Column<int>(type: "int", nullable: false),
                    WritingBookId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingOldSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WritingOldSession_WritingBook_WritingBookId",
                        column: x => x.WritingBookId,
                        principalTable: "WritingBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WritingOldSession_Writing_WritingId",
                        column: x => x.WritingId,
                        principalTable: "Writing",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FlashcardSessionRow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlashcardOldSessionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardSessionRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashcardSessionRow_FlashcardOldSession_FlashcardOldSessionId",
                        column: x => x.FlashcardOldSessionId,
                        principalTable: "FlashcardOldSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListeningSessionRow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListeningOldSessionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListenedSentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Similarity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeningSessionRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListeningSessionRow_ListeningOldSession_ListeningOldSessionId",
                        column: x => x.ListeningOldSessionId,
                        principalTable: "ListeningOldSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReadingSessionRow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReadingOldSessionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SelectedSentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerTranslate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Similarity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingSessionRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingSessionRow_ReadingOldSession_ReadingOldSessionId",
                        column: x => x.ReadingOldSessionId,
                        principalTable: "ReadingOldSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WritingSessionRow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WritingOldSessionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SelectedSentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerTranslate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Similarity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingSessionRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WritingSessionRow_WritingOldSession_WritingOldSessionId",
                        column: x => x.WritingOldSessionId,
                        principalTable: "WritingOldSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NativeLanguageId",
                table: "AspNetUsers",
                column: "NativeLanguageId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeckVideo_ListeningCategoryId",
                table: "DeckVideo",
                column: "ListeningCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckWord_FlashcardCategoryId",
                table: "DeckWord",
                column: "FlashcardCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Flashcard_LanguageId",
                table: "Flashcard",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Flashcard_PracticeId",
                table: "Flashcard",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Flashcard_UserId",
                table: "Flashcard",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardCategory_FlashcardId",
                table: "FlashcardCategory",
                column: "FlashcardId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardOldSession_FlashcardCategoryId",
                table: "FlashcardOldSession",
                column: "FlashcardCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardOldSession_FlashcardId",
                table: "FlashcardOldSession",
                column: "FlashcardId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardSessionRow_FlashcardOldSessionId",
                table: "FlashcardSessionRow",
                column: "FlashcardOldSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Listening_LanguageId",
                table: "Listening",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Listening_PracticeId",
                table: "Listening",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Listening_UserId",
                table: "Listening",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeningCategory_ListeningId",
                table: "ListeningCategory",
                column: "ListeningId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeningOldSession_ListeningCategoryId",
                table: "ListeningOldSession",
                column: "ListeningCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeningOldSession_ListeningId",
                table: "ListeningOldSession",
                column: "ListeningId");

            migrationBuilder.CreateIndex(
                name: "IX_ListeningSessionRow_ListeningOldSessionId",
                table: "ListeningSessionRow",
                column: "ListeningOldSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Practice_LanguageId",
                table: "Practice",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Reading_LanguageId",
                table: "Reading",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Reading_PracticeId",
                table: "Reading",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reading_UserId",
                table: "Reading",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingBook_ReadingId",
                table: "ReadingBook",
                column: "ReadingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingOldSession_ReadingBookId",
                table: "ReadingOldSession",
                column: "ReadingBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingOldSession_ReadingId",
                table: "ReadingOldSession",
                column: "ReadingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingSessionRow_ReadingOldSessionId",
                table: "ReadingSessionRow",
                column: "ReadingOldSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Writing_LanguageId",
                table: "Writing",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Writing_PracticeId",
                table: "Writing",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Writing_UserId",
                table: "Writing",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WritingBook_WritingId",
                table: "WritingBook",
                column: "WritingId");

            migrationBuilder.CreateIndex(
                name: "IX_WritingOldSession_WritingBookId",
                table: "WritingOldSession",
                column: "WritingBookId");

            migrationBuilder.CreateIndex(
                name: "IX_WritingOldSession_WritingId",
                table: "WritingOldSession",
                column: "WritingId");

            migrationBuilder.CreateIndex(
                name: "IX_WritingSessionRow_WritingOldSessionId",
                table: "WritingSessionRow",
                column: "WritingOldSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DeckVideo");

            migrationBuilder.DropTable(
                name: "DeckWord");

            migrationBuilder.DropTable(
                name: "FlashcardSessionRow");

            migrationBuilder.DropTable(
                name: "ListeningSessionRow");

            migrationBuilder.DropTable(
                name: "ReadingSessionRow");

            migrationBuilder.DropTable(
                name: "UserRefreshToken");

            migrationBuilder.DropTable(
                name: "WritingSessionRow");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FlashcardOldSession");

            migrationBuilder.DropTable(
                name: "ListeningOldSession");

            migrationBuilder.DropTable(
                name: "ReadingOldSession");

            migrationBuilder.DropTable(
                name: "WritingOldSession");

            migrationBuilder.DropTable(
                name: "FlashcardCategory");

            migrationBuilder.DropTable(
                name: "ListeningCategory");

            migrationBuilder.DropTable(
                name: "ReadingBook");

            migrationBuilder.DropTable(
                name: "WritingBook");

            migrationBuilder.DropTable(
                name: "Flashcard");

            migrationBuilder.DropTable(
                name: "Listening");

            migrationBuilder.DropTable(
                name: "Reading");

            migrationBuilder.DropTable(
                name: "Writing");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Practice");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
