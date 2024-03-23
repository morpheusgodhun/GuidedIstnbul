using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class additionalService_PriceDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalServiceOptionPrices");

            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptionPriceDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    AdditionalServiceOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceOptionPriceDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionPriceDates_AdditionalServiceOptions_AdditionalServiceOptionID",
                        column: x => x.AdditionalServiceOptionID,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionPriceDates_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPriceDates",
                column: "AdditionalServiceOptionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalServiceOptionPriceDates");

            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptionPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceOptionPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionPrices_AdditionalServiceOptions_AdditionalServiceOptionID",
                        column: x => x.AdditionalServiceOptionID,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionPrices_PersonPolicies_PersonPolicyID",
                        column: x => x.PersonPolicyID,
                        principalTable: "PersonPolicies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionPrices_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPrices",
                column: "AdditionalServiceOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionPrices_PersonPolicyID",
                table: "AdditionalServiceOptionPrices",
                column: "PersonPolicyID");
        }
    }
}
