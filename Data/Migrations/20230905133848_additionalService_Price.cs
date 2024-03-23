using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class additionalService_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptionPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PersonPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AdditionalServiceOptionPriceDateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceOptionPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionPrices_AdditionalServiceOptionPriceDates_AdditionalServiceOptionPriceDateID",
                        column: x => x.AdditionalServiceOptionPriceDateID,
                        principalTable: "AdditionalServiceOptionPriceDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionPrices_PersonPolicies_PersonPolicyID",
                        column: x => x.PersonPolicyID,
                        principalTable: "PersonPolicies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionPrices_AdditionalServiceOptionPriceDateID",
                table: "AdditionalServiceOptionPrices",
                column: "AdditionalServiceOptionPriceDateID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionPrices_PersonPolicyID",
                table: "AdditionalServiceOptionPrices",
                column: "PersonPolicyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalServiceOptionPrices");
        }
    }
}
