using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class tour_manies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExclusionIDs",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "InclusionIDs",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "SegmentID",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "SightToSeeIDs",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourTypeID",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "SuggestedStartTimeID",
                table: "Tours",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StartCityID",
                table: "Tours",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Many_Tour_Exclusions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Tour_Exclusions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Tour_Exclusions_SystemOptions_SystemOptionID",
                        column: x => x.SystemOptionID,
                        principalTable: "SystemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_Tour_Exclusions_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Tour_Inclusions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Tour_Inclusions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Tour_Inclusions_SystemOptions_SystemOptionID",
                        column: x => x.SystemOptionID,
                        principalTable: "SystemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_Tour_Inclusions_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Many_Tour_SightToSees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TourID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Tour_SightToSees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Tour_SightToSees_SystemOptions_SystemOptionID",
                        column: x => x.SystemOptionID,
                        principalTable: "SystemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_Tour_SightToSees_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_Exclusions_SystemOptionID",
                table: "Many_Tour_Exclusions",
                column: "SystemOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_Exclusions_TourID",
                table: "Many_Tour_Exclusions",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_Inclusions_SystemOptionID",
                table: "Many_Tour_Inclusions",
                column: "SystemOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_Inclusions_TourID",
                table: "Many_Tour_Inclusions",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_SightToSees_SystemOptionID",
                table: "Many_Tour_SightToSees",
                column: "SystemOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Tour_SightToSees_TourID",
                table: "Many_Tour_SightToSees",
                column: "TourID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Many_Tour_Exclusions");

            migrationBuilder.DropTable(
                name: "Many_Tour_Inclusions");

            migrationBuilder.DropTable(
                name: "Many_Tour_SightToSees");

            migrationBuilder.AlterColumn<string>(
                name: "SuggestedStartTimeID",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StartCityID",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExclusionIDs",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InclusionIDs",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SegmentID",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SightToSeeIDs",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TourTypeID",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
