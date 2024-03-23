using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class operationNullableFields2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationGuides_Guides_GuideId",
                table: "OperationGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationVehicles_Vehicles_VehicleId",
                table: "OperationVehicles");

            migrationBuilder.AlterColumn<Guid>(
                name: "VehicleId",
                table: "OperationVehicles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "GuideId",
                table: "OperationGuides",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationGuides_Guides_GuideId",
                table: "OperationGuides",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationVehicles_Vehicles_VehicleId",
                table: "OperationVehicles",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationGuides_Guides_GuideId",
                table: "OperationGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationVehicles_Vehicles_VehicleId",
                table: "OperationVehicles");

            migrationBuilder.AlterColumn<Guid>(
                name: "VehicleId",
                table: "OperationVehicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GuideId",
                table: "OperationGuides",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationGuides_Guides_GuideId",
                table: "OperationGuides",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationVehicles_Vehicles_VehicleId",
                table: "OperationVehicles",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
