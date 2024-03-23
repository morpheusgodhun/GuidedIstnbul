using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class GuideAndAgentApproveStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Guides");

            migrationBuilder.AddColumn<int>(
                name: "ApproveStatus",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApproveStatus",
                table: "Agents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultTourDiscount",
                table: "Agents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServicesDiscountRate",
                table: "Agents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WithoutPay",
                table: "Agents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveStatus",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "ApproveStatus",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "DefaultTourDiscount",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "ServicesDiscountRate",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "WithoutPay",
                table: "Agents");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Guides",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
