using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class add_TrialEntry_Field_FStation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 390, DateTimeKind.Local).AddTicks(4004),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 314, DateTimeKind.Local).AddTicks(8082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 400, DateTimeKind.Local).AddTicks(4259),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 325, DateTimeKind.Local).AddTicks(7791));

            migrationBuilder.AddColumn<string>(
                name: "FStation",
                table: "TrialEntry",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 399, DateTimeKind.Local).AddTicks(4007),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 324, DateTimeKind.Local).AddTicks(8105));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FStation",
                table: "TrialEntry");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 314, DateTimeKind.Local).AddTicks(8082),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 390, DateTimeKind.Local).AddTicks(4004));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 325, DateTimeKind.Local).AddTicks(7791),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 400, DateTimeKind.Local).AddTicks(4259));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 324, DateTimeKind.Local).AddTicks(8105),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 399, DateTimeKind.Local).AddTicks(4007));
        }
    }
}
