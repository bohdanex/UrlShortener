using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortenerDataAccess.Migrations
{
    public partial class AddedUrlsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceLocators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShortenedURL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OriginalURL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceLocators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceLocators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLocators_OriginalURL",
                table: "ResourceLocators",
                column: "OriginalURL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLocators_ShortenedURL",
                table: "ResourceLocators",
                column: "ShortenedURL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLocators_UserId",
                table: "ResourceLocators",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceLocators");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");
        }
    }
}
