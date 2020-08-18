using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.MVC.Migrations
{
    public partial class UpdatedRoleApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_ApplicationUserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_PrimaryRoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ApplicationUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "PrimaryRoleId",
                table: "Users",
                newName: "UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PrimaryRoleId",
                table: "Users",
                newName: "IX_Users_UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_UserRoleId",
                table: "Users",
                column: "UserRoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_UserRoleId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                table: "Users",
                newName: "PrimaryRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                newName: "IX_Users_PrimaryRoleId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ApplicationUserId",
                table: "Roles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_ApplicationUserId",
                table: "Roles",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_PrimaryRoleId",
                table: "Users",
                column: "PrimaryRoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
