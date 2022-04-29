using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class change_field_nextName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 372, DateTimeKind.Local).AddTicks(9192),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 288, DateTimeKind.Local).AddTicks(26));

            migrationBuilder.AlterColumn<string>(
                name: "NextName",
                table: "WFInstance",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 384, DateTimeKind.Local).AddTicks(9189),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 304, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 383, DateTimeKind.Local).AddTicks(9186),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 303, DateTimeKind.Local).AddTicks(40));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 288, DateTimeKind.Local).AddTicks(26),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 372, DateTimeKind.Local).AddTicks(9192));

            migrationBuilder.AlterColumn<string>(
                name: "NextName",
                table: "WFInstance",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 304, DateTimeKind.Local).AddTicks(74),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 384, DateTimeKind.Local).AddTicks(9189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 303, DateTimeKind.Local).AddTicks(40),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 383, DateTimeKind.Local).AddTicks(9186));
        }
    }
}
