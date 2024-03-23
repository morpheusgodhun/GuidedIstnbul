using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class BlogShowOnFAQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogLanguageItems_Language_LangaugeID",
                table: "BlogLanguageItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FaqCategoryLanguageItems_Language_LanguageId",
                table: "FaqCategoryLanguageItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FaqLangaugeItems_Language_LanguageId",
                table: "FaqLangaugeItems");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropIndex(
                name: "IX_FaqLangaugeItems_LanguageId",
                table: "FaqLangaugeItems");

            migrationBuilder.DropIndex(
                name: "IX_FaqCategoryLanguageItems_LanguageId",
                table: "FaqCategoryLanguageItems");

            migrationBuilder.DropIndex(
                name: "IX_BlogLanguageItems_LangaugeID",
                table: "BlogLanguageItems");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "FaqLangaugeItems");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "FaqCategoryLanguageItems");

            migrationBuilder.DropColumn(
                name: "LangaugeID",
                table: "BlogLanguageItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "ShowOnFAQ",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LanguageID",
                table: "BlogLanguageItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ShowOnFAQ",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "LanguageID",
                table: "BlogLanguageItems");

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "FaqLangaugeItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "FaqCategoryLanguageItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LangaugeID",
                table: "BlogLanguageItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBaseLanguage = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaqLangaugeItems_LanguageId",
                table: "FaqLangaugeItems",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_FaqCategoryLanguageItems_LanguageId",
                table: "FaqCategoryLanguageItems",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogLanguageItems_LangaugeID",
                table: "BlogLanguageItems",
                column: "LangaugeID");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogLanguageItems_Language_LangaugeID",
                table: "BlogLanguageItems",
                column: "LangaugeID",
                principalTable: "Language",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FaqCategoryLanguageItems_Language_LanguageId",
                table: "FaqCategoryLanguageItems",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FaqLangaugeItems_Language_LanguageId",
                table: "FaqLangaugeItems",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id");
        }
    }
}
