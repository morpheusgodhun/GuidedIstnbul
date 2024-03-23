using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class pageLanguageItemPropChangeblogCategorySlugPropAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LangaugeID",
                table: "PageLanguageItems",
                newName: "LanguageId");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "BlogCategoryLanguageItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "BlogCategoryLanguageItems");

            migrationBuilder.RenameColumn(
                name: "LanguageId",
                table: "PageLanguageItems",
                newName: "LangaugeID");
        }
    }
}
