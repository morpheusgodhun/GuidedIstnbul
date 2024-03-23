using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class tourchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_SystemOptions_SegmentID",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_SystemOptions_TourTypeID",
                table: "Tours");

            migrationBuilder.AlterColumn<Guid>(
                name: "TourTypeID",
                table: "Tours",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SegmentID",
                table: "Tours",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPerPerson",
                table: "Tours",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPerDay",
                table: "Tours",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_SystemOptions_SegmentID",
                table: "Tours",
                column: "SegmentID",
                principalTable: "SystemOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_SystemOptions_TourTypeID",
                table: "Tours",
                column: "TourTypeID",
                principalTable: "SystemOptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_SystemOptions_SegmentID",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_SystemOptions_TourTypeID",
                table: "Tours");

            migrationBuilder.AlterColumn<Guid>(
                name: "TourTypeID",
                table: "Tours",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SegmentID",
                table: "Tours",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsPerPerson",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "IsPerDay",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_SystemOptions_SegmentID",
                table: "Tours",
                column: "SegmentID",
                principalTable: "SystemOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_SystemOptions_TourTypeID",
                table: "Tours",
                column: "TourTypeID",
                principalTable: "SystemOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
