using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class many_blog_blogCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Many_Blog_BlogCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Blog_BlogCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Blog_BlogCategories_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Many_Blog_BlogCategories_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_BlogCategories_BlogCategoryId",
                table: "Many_Blog_BlogCategories",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Blog_BlogCategories_BlogId",
                table: "Many_Blog_BlogCategories",
                column: "BlogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Many_Blog_BlogCategories");
        }
    }
}
