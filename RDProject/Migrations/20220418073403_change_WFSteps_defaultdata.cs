using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class change_WFSteps_defaultdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "WFStep",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 405, DateTimeKind.Local).AddTicks(2598),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 13, 16, 50, 51, 658, DateTimeKind.Local).AddTicks(3142));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 415, DateTimeKind.Local).AddTicks(2660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 13, 16, 50, 51, 668, DateTimeKind.Local).AddTicks(2856));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 414, DateTimeKind.Local).AddTicks(2604),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 13, 16, 50, 51, 668, DateTimeKind.Local).AddTicks(2856));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "WFStep",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 13, 16, 50, 51, 658, DateTimeKind.Local).AddTicks(3142),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 405, DateTimeKind.Local).AddTicks(2598));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 13, 16, 50, 51, 668, DateTimeKind.Local).AddTicks(2856),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 415, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 13, 16, 50, 51, 668, DateTimeKind.Local).AddTicks(2856),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 414, DateTimeKind.Local).AddTicks(2604));
        }
    }
}
