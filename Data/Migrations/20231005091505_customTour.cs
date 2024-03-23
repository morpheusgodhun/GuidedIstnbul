using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class customTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomTourRequestId",
                table: "Tours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomTourRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParticipantNumber = table.Column<int>(type: "int", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    OfferStatusId = table.Column<int>(type: "int", nullable: false),
                    FindUsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTourRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomTourRequests_SystemOptions_FindUsId",
                        column: x => x.FindUsId,
                        principalTable: "SystemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomTourRequests_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfferTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferTemplateCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Program = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Many_CustomTourRequest_AlsoInteresteds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomTourRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_CustomTourRequest_AlsoInteresteds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_CustomTourRequest_AlsoInteresteds_CustomTourRequests_CustomTourRequestId",
                        column: x => x.CustomTourRequestId,
                        principalTable: "CustomTourRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_CustomTourRequest_AlsoInteresteds_SystemOptions_SystemOptionId",
                        column: x => x.SystemOptionId,
                        principalTable: "SystemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Many_CustomTourRequest_Destinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomTourRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_CustomTourRequest_Destinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_CustomTourRequest_Destinations_CustomTourRequests_CustomTourRequestId",
                        column: x => x.CustomTourRequestId,
                        principalTable: "CustomTourRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_CustomTourRequest_Destinations_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TourProgram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answered = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomTourRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_CustomTourRequests_CustomTourRequestId",
                        column: x => x.CustomTourRequestId,
                        principalTable: "CustomTourRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tours_CustomTourRequestId",
                table: "Tours",
                column: "CustomTourRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomTourRequests_FindUsId",
                table: "CustomTourRequests",
                column: "FindUsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomTourRequests_TourId",
                table: "CustomTourRequests",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_CustomTourRequest_AlsoInteresteds_CustomTourRequestId",
                table: "Many_CustomTourRequest_AlsoInteresteds",
                column: "CustomTourRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_CustomTourRequest_AlsoInteresteds_SystemOptionId",
                table: "Many_CustomTourRequest_AlsoInteresteds",
                column: "SystemOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_CustomTourRequest_Destinations_CustomTourRequestId",
                table: "Many_CustomTourRequest_Destinations",
                column: "CustomTourRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_CustomTourRequest_Destinations_DestinationId",
                table: "Many_CustomTourRequest_Destinations",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CustomTourRequestId",
                table: "Offers",
                column: "CustomTourRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_CustomTourRequests_CustomTourRequestId",
                table: "Tours",
                column: "CustomTourRequestId",
                principalTable: "CustomTourRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_CustomTourRequests_CustomTourRequestId",
                table: "Tours");

            migrationBuilder.DropTable(
                name: "Many_CustomTourRequest_AlsoInteresteds");

            migrationBuilder.DropTable(
                name: "Many_CustomTourRequest_Destinations");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "OfferTemplates");

            migrationBuilder.DropTable(
                name: "CustomTourRequests");

            migrationBuilder.DropIndex(
                name: "IX_Tours_CustomTourRequestId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CustomTourRequestId",
                table: "Tours");
        }
    }
}
