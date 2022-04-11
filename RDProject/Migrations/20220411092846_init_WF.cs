using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class init_WF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FCompany",
                table: "Trial",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WFInstance",
                columns: table => new
                {
                    HeadId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstanceId = table.Column<long>(nullable: false),
                    TableName = table.Column<string>(nullable: true),
                    InstanceGuid = table.Column<string>(nullable: true),
                    InstanceStatus = table.Column<bool>(nullable: false),
                    SubBy = table.Column<string>(nullable: true),
                    SubTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WFInstance", x => x.HeadId);
                });

            migrationBuilder.CreateTable(
                name: "WFStep",
                columns: table => new
                {
                    StepId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstanceId = table.Column<long>(nullable: false),
                    BookMark = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    SubBy = table.Column<string>(nullable: true),
                    SubTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WFStep", x => x.StepId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WFInstance");

            migrationBuilder.DropTable(
                name: "WFStep");

            migrationBuilder.DropColumn(
                name: "FCompany",
                table: "Trial");
        }
    }
}
