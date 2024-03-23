using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SystemOptionCategoryEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemOptions_SystemOptionCategories_SystemOptionCategoryID",
                table: "SystemOptions");

            migrationBuilder.DropTable(
                name: "SystemOptionCategories");

            migrationBuilder.DropIndex(
                name: "IX_SystemOptions_SystemOptionCategoryID",
                table: "SystemOptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemOptionCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOptionCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemOptions_SystemOptionCategoryID",
                table: "SystemOptions",
                column: "SystemOptionCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemOptions_SystemOptionCategories_SystemOptionCategoryID",
                table: "SystemOptions",
                column: "SystemOptionCategoryID",
                principalTable: "SystemOptionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
