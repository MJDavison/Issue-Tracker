using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.MVC.Migrations
{
    public partial class ChangedIssuemodeltouseDateTimeratherthanDateTimeoffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "IssueModel",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PostDate",
                table: "IssueModel",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
