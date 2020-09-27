using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.MVC.Migrations
{
    public partial class AddedPriorityTypeandStatustoTicketEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tickets");
        }
    }
}
