using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class reservationInputConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                table: "ReservationOptionInputs");

            migrationBuilder.AddColumn<Guid>(
                name: "AdditionalServiceInputId",
                table: "ReservationOptionInputs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOptionInputs_AdditionalServiceInputId",
                table: "ReservationOptionInputs",
                column: "AdditionalServiceInputId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationOptionInputs_AdditionalServiceInputs_AdditionalServiceInputId",
                table: "ReservationOptionInputs",
                column: "AdditionalServiceInputId",
                principalTable: "AdditionalServiceInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationOptionInputs_AdditionalServiceInputs_AdditionalServiceInputId",
                table: "ReservationOptionInputs");

            migrationBuilder.DropIndex(
                name: "IX_ReservationOptionInputs_AdditionalServiceInputId",
                table: "ReservationOptionInputs");

            migrationBuilder.DropColumn(
                name: "AdditionalServiceInputId",
                table: "ReservationOptionInputs");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "ReservationOptionInputs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
