using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addadditionalService1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalServiceInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    InputTypeID = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPerPerson = table.Column<bool>(type: "bit", nullable: false),
                    IsPerDay = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecialNumber = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceInputLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    AdditionalServiceInputID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceInputLanguageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceInputLanguageItems_AdditionalServiceInputs_AdditionalServiceInputID",
                        column: x => x.AdditionalServiceInputID,
                        principalTable: "AdditionalServiceInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    AdditionalServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceLanguageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceLanguageItems_AdditionalServices_AdditionalServiceID",
                        column: x => x.AdditionalServiceID,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptions_AdditionalServices_AdditionalServiceID",
                        column: x => x.AdditionalServiceID,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptionLanguageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false),
                    AdditionalServiceOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceOptionLanguageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionLanguageItems_AdditionalServiceOptions_AdditionalServiceOptionID",
                        column: x => x.AdditionalServiceOptionID,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptionPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    AdditionalServiceOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonPolicyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Many_AdditionalServiceOption_AdditionalServiceInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalServiceInputID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_AdditionalServiceOption_AdditionalServiceInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_AdditionalServiceOption_AdditionalServiceInputs_AdditionalServiceInputs_AdditionalServiceInputID",
                        column: x => x.AdditionalServiceInputID,
                        principalTable: "AdditionalServiceInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_AdditionalServiceOption_AdditionalServiceInputs_AdditionalServiceOptions_AdditionalServiceOptionID",
                        column: x => x.AdditionalServiceOptionID,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceInputLanguageItems_AdditionalServiceInputID",
                table: "AdditionalServiceInputLanguageItems",
                column: "AdditionalServiceInputID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceLanguageItems_AdditionalServiceID",
                table: "AdditionalServiceLanguageItems",
                column: "AdditionalServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionLanguageItems_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionLanguageItems",
                column: "AdditionalServiceOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionPrices_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPrices",
                column: "AdditionalServiceOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionPrices_PersonPolicyID",
                table: "AdditionalServiceOptionPrices",
                column: "PersonPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptions_AdditionalServiceID",
                table: "AdditionalServiceOptions",
                column: "AdditionalServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_AdditionalServiceOption_AdditionalServiceInputs_AdditionalServiceInputID",
                table: "Many_AdditionalServiceOption_AdditionalServiceInputs",
                column: "AdditionalServiceInputID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_AdditionalServiceOption_AdditionalServiceInputs_AdditionalServiceOptionID",
                table: "Many_AdditionalServiceOption_AdditionalServiceInputs",
                column: "AdditionalServiceOptionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalServiceInputLanguageItems");

            migrationBuilder.DropTable(
                name: "AdditionalServiceLanguageItems");

            migrationBuilder.DropTable(
                name: "AdditionalServiceOptionLanguageItems");

            migrationBuilder.DropTable(
                name: "AdditionalServiceOptionPrices");

            migrationBuilder.DropTable(
                name: "Many_AdditionalServiceOption_AdditionalServiceInputs");

            migrationBuilder.DropTable(
                name: "AdditionalServiceInputs");

            migrationBuilder.DropTable(
                name: "AdditionalServiceOptions");

            migrationBuilder.DropTable(
                name: "AdditionalServices");
        }
    }
}
