using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class reservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Many_Reservation_AdditionalServiceOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Reservation_AdditionalServiceOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Reservation_AdditionalServiceOptions_AdditionalServiceOptions_AdditionalServiceOptionId",
                        column: x => x.AdditionalServiceOptionId,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "ReservationOptionInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Many_Reservation_AdditionalServiceOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationOptionInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationOptionInputs_Many_Reservation_AdditionalServiceOptions_Many_Reservation_AdditionalServiceOptionId",
                        column: x => x.Many_Reservation_AdditionalServiceOptionId,
                        principalTable: "Many_Reservation_AdditionalServiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTimeId = table.Column<int>(type: "int", nullable: false),
                    ParticipantNumber = table.Column<int>(type: "int", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hotel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlsoInterestedNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickUpPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReservationNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Many_Reservation_AdditionalServiceOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationBillingInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FindUsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Many_Reservation_AdditionalServiceOptions_Many_Reservation_AdditionalServiceOptionId",
                        column: x => x.Many_Reservation_AdditionalServiceOptionId,
                        principalTable: "Many_Reservation_AdditionalServiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_ReservationBillingInfos_ReservationBillingInfoId",
                        column: x => x.ReservationBillingInfoId,
                        principalTable: "ReservationBillingInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_SystemOptions_FindUsId",
                        column: x => x.FindUsId,
                        principalTable: "SystemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_ReservationForm_AlsoInteresteds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_ReservationForm_AlsoInteresteds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_ReservationForm_AlsoInteresteds_Reservations_ReservationFormId",
                        column: x => x.ReservationFormId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_ReservationForm_AlsoInteresteds_SystemOptions_SystemOptionId",
                        column: x => x.SystemOptionId,
                        principalTable: "SystemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ReservationParticipants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationParticipants_Reservations_ReservationFormId",
                        column: x => x.ReservationFormId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DebtType = table.Column<int>(type: "int", nullable: true),
                    DiscountRate = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDebt = table.Column<bool>(type: "bit", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationPayments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationPayments_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationPayments_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Many_Reservation_AdditionalServiceOptions_AdditionalServiceOptionId",
                table: "Many_Reservation_AdditionalServiceOptions",
                column: "AdditionalServiceOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_ReservationForm_AlsoInteresteds_ReservationFormId",
                table: "Many_ReservationForm_AlsoInteresteds",
                column: "ReservationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_ReservationForm_AlsoInteresteds_SystemOptionId",
                table: "Many_ReservationForm_AlsoInteresteds",
                column: "SystemOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationOptionInputs_Many_Reservation_AdditionalServiceOptionId",
                table: "ReservationOptionInputs",
                column: "Many_Reservation_AdditionalServiceOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationParticipants_ReservationFormId",
                table: "ReservationParticipants",
                column: "ReservationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationPayments_PaymentMethodId",
                table: "ReservationPayments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationPayments_PaymentTypeId",
                table: "ReservationPayments",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationPayments_ReservationId",
                table: "ReservationPayments",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FindUsId",
                table: "Reservations",
                column: "FindUsId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Many_ReservationForm_AlsoInteresteds");

            migrationBuilder.DropTable(
                name: "ReservationOptionInputs");

            migrationBuilder.DropTable(
                name: "ReservationParticipants");

            migrationBuilder.DropTable(
                name: "ReservationPayments");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Many_Reservation_AdditionalServiceOptions");

            migrationBuilder.DropTable(
                name: "ReservationBillingInfos");
        }
    }
}
