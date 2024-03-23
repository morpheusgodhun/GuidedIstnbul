using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class faqCategoryPageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PageId",
                table: "FaqCategories",
                type: "uniqueidentifier",
                nullable: true);
            migrationBuilder.CreateIndex(
                name: "IX_FaqCategories_PageId",
                table: "FaqCategories",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaqCategories_Pages_PageId",
                table: "FaqCategories",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaqCategories_Pages_PageId",
                table: "FaqCategories");

            migrationBuilder.DropIndex(
                name: "IX_FaqCategories_PageId",
                table: "FaqCategories");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "FaqCategories");
        }
    }
}
