using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class discountAddingDateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UsingEndDate",
                table: "Discounts",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UsingStartDate",
                table: "Discounts",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsingEndDate",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "UsingStartDate",
                table: "Discounts");
        }
    }
}
