using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class reservationAgent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AgentId",
                table: "Reservations",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Agents_AgentId",
                table: "Reservations",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Agents_AgentId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_AgentId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Reservations");
        }
    }
}
