using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class AddSomeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FCreateUser",
                table: "Trial",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FTitle",
                table: "Trial",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FCreateUser",
                table: "Trial");

            migrationBuilder.DropColumn(
                name: "FTitle",
                table: "Trial");
        }
    }
}
