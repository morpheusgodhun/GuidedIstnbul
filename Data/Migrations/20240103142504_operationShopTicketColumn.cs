using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class operationShopTicketColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "OperationTickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "OperationShops",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "OperationNotes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTickets_OperationId",
                table: "OperationTickets",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationShops_OperationId",
                table: "OperationShops",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationShops_Operations_OperationId",
                table: "OperationShops",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTickets_Operations_OperationId",
                table: "OperationTickets",
                column: "OperationId",
                principalTable: "Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationShops_Operations_OperationId",
                table: "OperationShops");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationTickets_Operations_OperationId",
                table: "OperationTickets");

            migrationBuilder.DropIndex(
                name: "IX_OperationTickets_OperationId",
                table: "OperationTickets");

            migrationBuilder.DropIndex(
                name: "IX_OperationShops_OperationId",
                table: "OperationShops");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "OperationTickets");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "OperationShops");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "OperationNotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
