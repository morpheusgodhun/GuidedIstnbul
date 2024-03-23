using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class reservation_nullableBillingInfo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReservationBillingInfoId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReservationBillingInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsIndividual = table.Column<bool>(type: "bit", nullable: false),
                    BillingFullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TcOrPassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingTaxAdministration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingTaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationBillingInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationBillingInfoId",
                table: "Reservations",
                column: "ReservationBillingInfoId",
                unique: true,
                filter: "[ReservationBillingInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationBillingInfos_ReservationBillingInfoId",
                table: "Reservations",
                column: "ReservationBillingInfoId",
                principalTable: "ReservationBillingInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationBillingInfos_ReservationBillingInfoId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationBillingInfos");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationBillingInfoId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationBillingInfoId",
                table: "Reservations");
        }
    }
}
