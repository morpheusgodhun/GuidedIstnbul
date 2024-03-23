using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Role_Template : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Many_User_Roles");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleTemplateId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RoleTemplates",
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
                    table.PrimaryKey("PK_RoleTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Many_Role_RoleTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_Role_RoleTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_Role_RoleTemplates_RoleTemplates_RoleTemplateId",
                        column: x => x.RoleTemplateId,
                        principalTable: "RoleTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_Role_RoleTemplates_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleTemplateId",
                table: "Users",
                column: "RoleTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Role_RoleTemplates_RoleId",
                table: "Many_Role_RoleTemplates",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Many_Role_RoleTemplates_RoleTemplateId",
                table: "Many_Role_RoleTemplates",
                column: "RoleTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RoleTemplates_RoleTemplateId",
                table: "Users",
                column: "RoleTemplateId",
                principalTable: "RoleTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RoleTemplates_RoleTemplateId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Many_Role_RoleTemplates");

            migrationBuilder.DropTable(
                name: "RoleTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleTemplateId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleTemplateId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Many_User_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Many_User_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Many_User_Roles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Many_User_Roles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Many_User_Roles_RoleID",
                table: "Many_User_Roles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Many_User_Roles_UserID",
                table: "Many_User_Roles",
                column: "UserID");
        }
    }
}
