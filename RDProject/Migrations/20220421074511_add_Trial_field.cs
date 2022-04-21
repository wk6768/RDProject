using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class add_Trial_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 558, DateTimeKind.Local).AddTicks(162),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 133, DateTimeKind.Local).AddTicks(5370));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "WFInstance",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 567, DateTimeKind.Local).AddTicks(455),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 143, DateTimeKind.Local).AddTicks(5365));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 567, DateTimeKind.Local).AddTicks(455),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 143, DateTimeKind.Local).AddTicks(5365));

            migrationBuilder.AddColumn<int>(
                name: "FStatus",
                table: "Trial",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FStatus",
                table: "Trial");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 133, DateTimeKind.Local).AddTicks(5370),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 558, DateTimeKind.Local).AddTicks(162));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "WFInstance",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 143, DateTimeKind.Local).AddTicks(5365),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 567, DateTimeKind.Local).AddTicks(455));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 20, 13, 42, 41, 143, DateTimeKind.Local).AddTicks(5365),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 567, DateTimeKind.Local).AddTicks(455));
        }
    }
}
