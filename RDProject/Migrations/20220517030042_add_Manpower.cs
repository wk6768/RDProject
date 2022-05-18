using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class add_Manpower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 11, 0, 42, 333, DateTimeKind.Local).AddTicks(8509),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 584, DateTimeKind.Local).AddTicks(6506));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 11, 0, 42, 339, DateTimeKind.Local).AddTicks(8824),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 595, DateTimeKind.Local).AddTicks(6504));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 11, 0, 42, 338, DateTimeKind.Local).AddTicks(8803),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 594, DateTimeKind.Local).AddTicks(6503));

            migrationBuilder.CreateTable(
                name: "Manpower",
                columns: table => new
                {
                    FHeadId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FCreateUser = table.Column<string>(maxLength: 16, nullable: true),
                    FCreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2022, 5, 17, 11, 0, 42, 326, DateTimeKind.Local).AddTicks(8843)),
                    FTitle = table.Column<string>(maxLength: 64, nullable: true),
                    FStatus = table.Column<int>(nullable: false),
                    FDate = table.Column<DateTime>(nullable: false),
                    FCompany = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manpower", x => x.FHeadId);
                });

            migrationBuilder.CreateTable(
                name: "ManpowerEntry",
                columns: table => new
                {
                    FEntryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FHeadId = table.Column<long>(nullable: false),
                    FCreateDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2022, 5, 17, 11, 0, 42, 332, DateTimeKind.Local).AddTicks(8797)),
                    FEmpId = table.Column<string>(nullable: true),
                    FEmpName = table.Column<string>(maxLength: 16, nullable: true),
                    FDeptName = table.Column<string>(maxLength: 16, nullable: true),
                    FAttendanceHours = table.Column<decimal>(nullable: false),
                    FNormalOvertimeHours = table.Column<decimal>(nullable: false),
                    FWeekendOvertimeHours = table.Column<decimal>(nullable: false),
                    FHolidayOvertimeHours = table.Column<decimal>(nullable: false),
                    FTotalHours = table.Column<decimal>(nullable: false),
                    FVarianceHours = table.Column<decimal>(nullable: false),
                    FRD28Hours = table.Column<decimal>(nullable: false),
                    FRD30Hours = table.Column<decimal>(nullable: false),
                    FRD31Hours = table.Column<decimal>(nullable: false),
                    FRD32Hours = table.Column<decimal>(nullable: false),
                    FRD33Hours = table.Column<decimal>(nullable: false),
                    FRD34Hours = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManpowerEntry", x => x.FEntryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manpower_FCompany",
                table: "Manpower",
                column: "FCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Manpower_FCreateDate",
                table: "Manpower",
                column: "FCreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Manpower_FCreateUser",
                table: "Manpower",
                column: "FCreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_Manpower_FDate",
                table: "Manpower",
                column: "FDate");

            migrationBuilder.CreateIndex(
                name: "IX_Manpower_FStatus",
                table: "Manpower",
                column: "FStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Manpower_FTitle",
                table: "Manpower",
                column: "FTitle");

            migrationBuilder.CreateIndex(
                name: "IX_ManpowerEntry_FHeadId",
                table: "ManpowerEntry",
                column: "FHeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manpower");

            migrationBuilder.DropTable(
                name: "ManpowerEntry");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 584, DateTimeKind.Local).AddTicks(6506),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 17, 11, 0, 42, 333, DateTimeKind.Local).AddTicks(8509));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 595, DateTimeKind.Local).AddTicks(6504),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 17, 11, 0, 42, 339, DateTimeKind.Local).AddTicks(8824));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 594, DateTimeKind.Local).AddTicks(6503),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 17, 11, 0, 42, 338, DateTimeKind.Local).AddTicks(8803));
        }
    }
}
