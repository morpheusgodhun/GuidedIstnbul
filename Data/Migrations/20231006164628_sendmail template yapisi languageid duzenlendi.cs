using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class sendmailtemplateyapisilanguageidduzenlendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "SendMailTemplates");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "SendMailTemplateLanguageItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "SendMailTemplateLanguageItems");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "SendMailTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
