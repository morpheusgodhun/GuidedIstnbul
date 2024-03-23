using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class productRoleTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Many_Product_RoleTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Product_RoleTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Product_RoleTemplates_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_Product_RoleTemplates_RoleTemplates_RoleTemplateId",
                        column: x => x.RoleTemplateId,
                        principalTable: "RoleTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_RoleTemplates_ProductId",
                table: "Many_Product_RoleTemplates",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Product_RoleTemplates_RoleTemplateId",
                table: "Many_Product_RoleTemplates",
                column: "RoleTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Many_Product_RoleTemplates");
        }
    }
}
