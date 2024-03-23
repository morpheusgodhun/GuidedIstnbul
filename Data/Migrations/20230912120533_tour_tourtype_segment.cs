using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class tour_tourtype_segment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SegmentID",
                table: "Tours",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TourTypeID",
                table: "Tours",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tours_SegmentID",
                table: "Tours",
                column: "SegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourTypeID",
                table: "Tours",
                column: "TourTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_SystemOptions_SegmentID",
                table: "Tours",
                column: "SegmentID",
                principalTable: "SystemOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_SystemOptions_TourTypeID",
                table: "Tours",
                column: "TourTypeID",
                principalTable: "SystemOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.DropIndex(
                name: "IX_Tours_SegmentID",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_TourTypeID",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "SegmentID",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourTypeID",
                table: "Tours");
        }
    }
}
