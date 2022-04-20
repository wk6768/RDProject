using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class add_WFStep_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StepNo",
                table: "WFStep",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 133, DateTimeKind.Local).AddTicks(5370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 405, DateTimeKind.Local).AddTicks(2598));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 143, DateTimeKind.Local).AddTicks(5365),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 415, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 143, DateTimeKind.Local).AddTicks(5365),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 414, DateTimeKind.Local).AddTicks(2604));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepNo",
                table: "WFStep");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 405, DateTimeKind.Local).AddTicks(2598),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 133, DateTimeKind.Local).AddTicks(5370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 415, DateTimeKind.Local).AddTicks(2660),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 143, DateTimeKind.Local).AddTicks(5365));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 18, 15, 34, 3, 414, DateTimeKind.Local).AddTicks(2604),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 143, DateTimeKind.Local).AddTicks(5365));
        }
    }
}
