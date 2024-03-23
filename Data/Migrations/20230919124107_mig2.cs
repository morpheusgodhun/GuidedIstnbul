using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourPrices_Tours_TourID",
                table: "TourPrices");

            migrationBuilder.AlterColumn<Guid>(
                name: "TourID",
                table: "TourPrices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TourPrices_Tours_TourID",
                table: "TourPrices",
                column: "TourID",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourPrices_Tours_TourID",
                table: "TourPrices");

            migrationBuilder.AlterColumn<Guid>(
                name: "TourID",
                table: "TourPrices",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TourPrices_Tours_TourID",
                table: "TourPrices",
                column: "TourID",
                principalTable: "Tours",
                principalColumn: "Id");
        }
    }
}
