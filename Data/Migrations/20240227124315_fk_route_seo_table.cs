using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fk_route_seo_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RouteId",
                table: "Seos",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Seos_RouteId",
                table: "Seos",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seos_Routes_RouteId",
                table: "Seos",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seos_Routes_RouteId",
                table: "Seos");

            migrationBuilder.DropIndex(
                name: "IX_Seos_RouteId",
                table: "Seos");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Seos");
        }
    }
}
