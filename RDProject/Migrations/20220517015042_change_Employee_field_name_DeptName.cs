using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class change_Employee_field_name_DeptName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmpName",
                table: "Employee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 584, DateTimeKind.Local).AddTicks(6506),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 549, DateTimeKind.Local).AddTicks(1772));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 595, DateTimeKind.Local).AddTicks(6504),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 560, DateTimeKind.Local).AddTicks(1753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 594, DateTimeKind.Local).AddTicks(6503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 559, DateTimeKind.Local).AddTicks(1745));

            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "Employee",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TrialReports",
                columns: table => new
                {
                    FDate = table.Column<DateTime>(nullable: false),
                    FBillNo = table.Column<string>(nullable: true),
                    FRDNo = table.Column<string>(nullable: true),
                    FWorkOrder = table.Column<string>(nullable: true),
                    FStation = table.Column<string>(nullable: true),
                    FAmount = table.Column<int>(nullable: false),
                    FManHours = table.Column<decimal>(nullable: false),
                    FBeginDate = table.Column<DateTime>(nullable: false),
                    FEndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrialReports");

            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "Employee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 549, DateTimeKind.Local).AddTicks(1772),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 584, DateTimeKind.Local).AddTicks(6506));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 560, DateTimeKind.Local).AddTicks(1753),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 595, DateTimeKind.Local).AddTicks(6504));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 559, DateTimeKind.Local).AddTicks(1745),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 50, 42, 594, DateTimeKind.Local).AddTicks(6503));

            migrationBuilder.AddColumn<string>(
                name: "EmpName",
                table: "Employee",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }
    }
}
