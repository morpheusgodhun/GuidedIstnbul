using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class tourPriceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourPrices_PersonPolicies_PersonPolicyID",
                table: "TourPrices");

            migrationBuilder.DropIndex(
                name: "IX_TourPrices_PersonPolicyID",
                table: "TourPrices");

            migrationBuilder.DropColumn(
                name: "PersonPolicyID",
                table: "TourPrices");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TourPrices");

            migrationBuilder.CreateTable(
                name: "TourPriceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TourPriceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPriceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPriceItems_PersonPolicies_PersonPolicyID",
                        column: x => x.PersonPolicyID,
                        principalTable: "PersonPolicies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourPriceItems_TourPrices_TourPriceID",
                        column: x => x.TourPriceID,
                        principalTable: "TourPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourPriceItems_PersonPolicyID",
                table: "TourPriceItems",
                column: "PersonPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_TourPriceItems_TourPriceID",
                table: "TourPriceItems",
                column: "TourPriceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourPriceItems");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonPolicyID",
                table: "TourPrices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TourPrices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_TourPrices_PersonPolicyID",
                table: "TourPrices",
                column: "PersonPolicyID");

            migrationBuilder.AddForeignKey(
                name: "FK_TourPrices_PersonPolicies_PersonPolicyID",
                table: "TourPrices",
                column: "PersonPolicyID",
                principalTable: "PersonPolicies",
                principalColumn: "Id");
        }
    }
}
