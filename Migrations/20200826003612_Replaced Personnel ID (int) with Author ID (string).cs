using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.MVC.Migrations
{
    public partial class ReplacedPersonnelIDintwithAuthorIDstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonnelId",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "PersonnelId",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);
        }
    }
}
