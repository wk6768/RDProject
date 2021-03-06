using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class init_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 16, nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    EmpName = table.Column<string>(maxLength: 32, nullable: false),
                    Pwd = table.Column<string>(maxLength: 128, nullable: false),
                    UserGroup = table.Column<string>(maxLength: 32, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trial",
                columns: table => new
                {
                    FHeadId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FDate = table.Column<DateTime>(nullable: false),
                    FBillNo = table.Column<string>(nullable: true),
                    FRDNo = table.Column<string>(nullable: true),
                    FProductName = table.Column<string>(nullable: true),
                    FWorkerOrderDescription = table.Column<string>(nullable: true),
                    FCompany = table.Column<string>(nullable: true),
                    FHasCNC = table.Column<bool>(nullable: false),
                    FHasCoating = table.Column<bool>(nullable: false),
                    FHasLaser = table.Column<bool>(nullable: false),
                    FHasAssembly = table.Column<bool>(nullable: false),
                    FCNCNPI = table.Column<string>(nullable: true),
                    FCoatingNPI = table.Column<string>(nullable: true),
                    FLaserNPI = table.Column<string>(nullable: true),
                    FAssemblyNPI = table.Column<string>(nullable: true),
                    FAssemblyFactory = table.Column<string>(nullable: true),
                    FInformation = table.Column<string>(nullable: true),
                    FRequire = table.Column<string>(nullable: true),
                    FCreateUser = table.Column<string>(nullable: true),
                    FCreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2022, 4, 13, 16, 50, 51, 668, DateTimeKind.Local).AddTicks(2856)),
                    FTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trial", x => x.FHeadId);
                });

            migrationBuilder.CreateTable(
                name: "TrialEntry",
                columns: table => new
                {
                    FEntryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FHeadId = table.Column<long>(nullable: false),
                    FWorkOrder = table.Column<string>(nullable: true),
                    FProcessName = table.Column<string>(nullable: true),
                    FBeginDate = table.Column<DateTime>(nullable: false),
                    FEndDate = table.Column<DateTime>(nullable: false),
                    FAmount = table.Column<int>(nullable: false),
                    FManPower = table.Column<decimal>(nullable: false),
                    FManHours = table.Column<decimal>(nullable: false),
                    FCreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2022, 4, 13, 16, 50, 51, 668, DateTimeKind.Local).AddTicks(2856))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrialEntry", x => x.FEntryId);
                });

            migrationBuilder.CreateTable(
                name: "WFInstance",
                columns: table => new
                {
                    InstanceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(nullable: true),
                    InstanceGuid = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    HeadId = table.Column<long>(nullable: false),
                    SubBy = table.Column<string>(nullable: true),
                    SubTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2022, 4, 13, 16, 50, 51, 658, DateTimeKind.Local).AddTicks(3142))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WFInstance", x => x.InstanceId);
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
                    SubTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WFStep", x => x.StepId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IsDeleted",
                table: "Employee",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Trial");

            migrationBuilder.DropTable(
                name: "TrialEntry");

            migrationBuilder.DropTable(
                name: "WFInstance");

            migrationBuilder.DropTable(
                name: "WFStep");
        }
    }
}
