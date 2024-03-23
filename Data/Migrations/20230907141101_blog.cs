using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class blog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BlogCategories_BlogCategoryID",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogLanguageItem_Blog_BlogID",
                table: "BlogLanguageItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogLanguageItem_Language_LangaugeID",
                table: "BlogLanguageItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Many_Blog_Tags_Blog_BlogID",
                table: "Many_Blog_Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogLanguageItem",
                table: "BlogLanguageItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "BlogLanguageItem",
                newName: "BlogLanguageItems");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_BlogLanguageItem_LangaugeID",
                table: "BlogLanguageItems",
                newName: "IX_BlogLanguageItems_LangaugeID");

            migrationBuilder.RenameIndex(
                name: "IX_BlogLanguageItem_BlogID",
                table: "BlogLanguageItems",
                newName: "IX_BlogLanguageItems_BlogID");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_BlogCategoryID",
                table: "Blogs",
                newName: "IX_Blogs_BlogCategoryID");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogID",
                table: "BlogLanguageItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: null,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogLanguageItems",
                table: "BlogLanguageItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogLanguageItems_Blogs_BlogID",
                table: "BlogLanguageItems",
                column: "BlogID",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogLanguageItems_Language_LangaugeID",
                table: "BlogLanguageItems",
                column: "LangaugeID",
                principalTable: "Language",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryID",
                table: "Blogs",
                column: "BlogCategoryID",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Blog_Tags_Blogs_BlogID",
                table: "Many_Blog_Tags",
                column: "BlogID",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogLanguageItems_Blogs_BlogID",
                table: "BlogLanguageItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogLanguageItems_Language_LangaugeID",
                table: "BlogLanguageItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryID",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Many_Blog_Tags_Blogs_BlogID",
                table: "Many_Blog_Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogLanguageItems",
                table: "BlogLanguageItems");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameTable(
                name: "BlogLanguageItems",
                newName: "BlogLanguageItem");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_BlogCategoryID",
                table: "Blog",
                newName: "IX_Blog_BlogCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_BlogLanguageItems_LangaugeID",
                table: "BlogLanguageItem",
                newName: "IX_BlogLanguageItem_LangaugeID");

            migrationBuilder.RenameIndex(
                name: "IX_BlogLanguageItems_BlogID",
                table: "BlogLanguageItem",
                newName: "IX_BlogLanguageItem_BlogID");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogID",
                table: "BlogLanguageItem",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogLanguageItem",
                table: "BlogLanguageItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BlogCategories_BlogCategoryID",
                table: "Blog",
                column: "BlogCategoryID",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogLanguageItem_Blog_BlogID",
                table: "BlogLanguageItem",
                column: "BlogID",
                principalTable: "Blog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogLanguageItem_Language_LangaugeID",
                table: "BlogLanguageItem",
                column: "LangaugeID",
                principalTable: "Language",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Blog_Tags_Blog_BlogID",
                table: "Many_Blog_Tags",
                column: "BlogID",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
