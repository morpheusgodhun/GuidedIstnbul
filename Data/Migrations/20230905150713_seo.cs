using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class seo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalServiceOptionPriceDates_AdditionalServiceOptions_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPriceDates");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "AdditionalServiceOptionPriceDates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPriceDates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: null,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Seos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalServiceOptionPriceDates_AdditionalServiceOptions_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPriceDates",
                column: "AdditionalServiceOptionID",
                principalTable: "AdditionalServiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalServiceOptionPriceDates_AdditionalServiceOptions_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPriceDates");

            migrationBuilder.DropTable(
                name: "Seos");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "AdditionalServiceOptionPriceDates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPriceDates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalServiceOptionPriceDates_AdditionalServiceOptions_AdditionalServiceOptionID",
                table: "AdditionalServiceOptionPriceDates",
                column: "AdditionalServiceOptionID",
                principalTable: "AdditionalServiceOptions",
                principalColumn: "Id");
        }
    }
}
