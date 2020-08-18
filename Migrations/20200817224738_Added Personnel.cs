using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.MVC.Migrations
{
    public partial class AddedPersonnel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Project_ProjectId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Project_ProjectId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProjectId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ProjectId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Issues");

            migrationBuilder.AddColumn<int>(
                name: "PersonnelId",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Personnel",
                columns: table => new
                {
                    PersonnelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.PersonnelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_PersonnelId",
                table: "Project",
                column: "PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Personnel_PersonnelId",
                table: "Project",
                column: "PersonnelId",
                principalTable: "Personnel",
                principalColumn: "PersonnelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Personnel_PersonnelId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "Personnel");

            migrationBuilder.DropIndex(
                name: "IX_Project_PersonnelId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "PersonnelId",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Issues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectId",
                table: "Users",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProjectId",
                table: "Issues",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Project_ProjectId",
                table: "Issues",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Project_ProjectId",
                table: "Users",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
