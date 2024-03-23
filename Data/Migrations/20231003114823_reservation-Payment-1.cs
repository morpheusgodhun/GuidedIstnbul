using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class reservationPayment1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationPayments_PaymentMethods_PaymentMethodId",
                table: "ReservationPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationPayments_PaymentType_PaymentTypeId",
                table: "ReservationPayments");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropIndex(
                name: "IX_ReservationPayments_PaymentMethodId",
                table: "ReservationPayments");

            migrationBuilder.DropIndex(
                name: "IX_ReservationPayments_PaymentTypeId",
                table: "ReservationPayments");

            migrationBuilder.DropColumn(
                name: "DebtType",
                table: "ReservationPayments");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "ReservationPayments");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "ReservationPayments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DebtType",
                table: "ReservationPayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "ReservationPayments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentTypeId",
                table: "ReservationPayments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationPayments_PaymentMethodId",
                table: "ReservationPayments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationPayments_PaymentTypeId",
                table: "ReservationPayments",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationPayments_PaymentMethods_PaymentMethodId",
                table: "ReservationPayments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationPayments_PaymentType_PaymentTypeId",
                table: "ReservationPayments",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
