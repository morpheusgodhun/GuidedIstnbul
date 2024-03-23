using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class requesteuseridekledim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CustomTourRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomTourRequests_UserId",
                table: "CustomTourRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomTourRequests_Users_UserId",
                table: "CustomTourRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomTourRequests_Users_UserId",
                table: "CustomTourRequests");

            migrationBuilder.DropIndex(
                name: "IX_CustomTourRequests_UserId",
                table: "CustomTourRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomTourRequests");
        }
    }
}
