using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class roleToRolePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Many_Role_RoleTemplates_Roles_RoleId",
                table: "Many_Role_RoleTemplates");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Many_Role_RoleTemplates",
                newName: "RolePermissionId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Many_Role_RoleTemplates_RoleId",
            //    table: "Many_Role_RoleTemplates",
            //    newName: "IX_Many_Role_RoleTemplates_RolePermissionId");

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Role_RoleTemplates_RolePermissions_RolePermissionId",
                table: "Many_Role_RoleTemplates",
                column: "RolePermissionId",
                principalTable: "RolePermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Many_Role_RoleTemplates_RolePermissions_RolePermissionId",
                table: "Many_Role_RoleTemplates");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.RenameColumn(
                name: "RolePermissionId",
                table: "Many_Role_RoleTemplates",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Many_Role_RoleTemplates_RolePermissionId",
                table: "Many_Role_RoleTemplates",
                newName: "IX_Many_Role_RoleTemplates_RoleId");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Many_Role_RoleTemplates_Roles_RoleId",
                table: "Many_Role_RoleTemplates",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
