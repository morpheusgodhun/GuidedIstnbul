using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class additionalServiceInput : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdditionalServiceID",
                table: "AdditionalServiceInputs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: null);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceInputs_AdditionalServiceID",
                table: "AdditionalServiceInputs",
                column: "AdditionalServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalServiceInputs_AdditionalServices_AdditionalServiceID",
                table: "AdditionalServiceInputs",
                column: "AdditionalServiceID",
                principalTable: "AdditionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalServiceInputs_AdditionalServices_AdditionalServiceID",
                table: "AdditionalServiceInputs");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalServiceInputs_AdditionalServiceID",
                table: "AdditionalServiceInputs");

            migrationBuilder.DropColumn(
                name: "AdditionalServiceID",
                table: "AdditionalServiceInputs");
        }
    }
}
