using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class reservation_nullableBillingInfo1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Many_Reservation_AdditionalServiceOptions_Many_Reservation_AdditionalServiceOptionId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationBillingInfos_ReservationBillingInfoId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationBillingInfos");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_Many_Reservation_AdditionalServiceOptionId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationBillingInfoId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Many_Reservation_AdditionalServiceOptionId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationBillingInfoId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Reservation_AdditionalServiceOptions_ReservationId",
                table: "Many_Reservation_AdditionalServiceOptions",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Reservation_AdditionalServiceOptions_Reservations_ReservationId",
                table: "Many_Reservation_AdditionalServiceOptions",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Many_Reservation_AdditionalServiceOptions_Reservations_ReservationId",
                table: "Many_Reservation_AdditionalServiceOptions");

            migrationBuilder.DropIndex(
                name: "IX_Many_Reservation_AdditionalServiceOptions_ReservationId",
                table: "Many_Reservation_AdditionalServiceOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "Many_Reservation_AdditionalServiceOptionId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationBillingInfoId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ReservationBillingInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingFullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingTaxAdministration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingTaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsIndividual = table.Column<bool>(type: "bit", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TcOrPassportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationBillingInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Many_Reservation_AdditionalServiceOptionId",
                table: "Reservations",
                column: "Many_Reservation_AdditionalServiceOptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationBillingInfoId",
                table: "Reservations",
                column: "ReservationBillingInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Many_Reservation_AdditionalServiceOptions_Many_Reservation_AdditionalServiceOptionId",
                table: "Reservations",
                column: "Many_Reservation_AdditionalServiceOptionId",
                principalTable: "Many_Reservation_AdditionalServiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationBillingInfos_ReservationBillingInfoId",
                table: "Reservations",
                column: "ReservationBillingInfoId",
                principalTable: "ReservationBillingInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
