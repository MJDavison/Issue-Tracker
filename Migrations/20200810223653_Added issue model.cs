using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.MVC.Migrations
{
    public partial class Addedissuemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Issues",
                table: "Issues");

            migrationBuilder.RenameTable(
                name: "Issues",
                newName: "IssueModel");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "IssueModel",
                newName: "IssueId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "IssueModel",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueModel",
                table: "IssueModel",
                column: "IssueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueModel",
                table: "IssueModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "IssueModel");

            migrationBuilder.RenameTable(
                name: "IssueModel",
                newName: "Issues");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Issues",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issues",
                table: "Issues",
                column: "Id");
        }
    }
}
