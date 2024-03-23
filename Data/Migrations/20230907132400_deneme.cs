using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class deneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstantValues_Page_PageID",
                table: "ConstantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PageLanguageItem_Page_PageID",
                table: "PageLanguageItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageLanguageItem",
                table: "PageLanguageItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Page",
                table: "Page");

            migrationBuilder.RenameTable(
                name: "PageLanguageItem",
                newName: "PageLanguageItems");

            migrationBuilder.RenameTable(
                name: "Page",
                newName: "Pages");

            migrationBuilder.RenameIndex(
                name: "IX_PageLanguageItem_PageID",
                table: "PageLanguageItems",
                newName: "IX_PageLanguageItems_PageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageLanguageItems",
                table: "PageLanguageItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstantValues_Pages_PageID",
                table: "ConstantValues",
                column: "PageID",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageLanguageItems_Pages_PageID",
                table: "PageLanguageItems",
                column: "PageID",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstantValues_Pages_PageID",
                table: "ConstantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PageLanguageItems_Pages_PageID",
                table: "PageLanguageItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageLanguageItems",
                table: "PageLanguageItems");

            migrationBuilder.RenameTable(
                name: "Pages",
                newName: "Page");

            migrationBuilder.RenameTable(
                name: "PageLanguageItems",
                newName: "PageLanguageItem");

            migrationBuilder.RenameIndex(
                name: "IX_PageLanguageItems_PageID",
                table: "PageLanguageItem",
                newName: "IX_PageLanguageItem_PageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Page",
                table: "Page",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageLanguageItem",
                table: "PageLanguageItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstantValues_Page_PageID",
                table: "ConstantValues",
                column: "PageID",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageLanguageItem_Page_PageID",
                table: "PageLanguageItem",
                column: "PageID",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
