using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class add_TrialEntry_Field_FStation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 549, DateTimeKind.Local).AddTicks(1772),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 390, DateTimeKind.Local).AddTicks(4004));

            migrationBuilder.AlterColumn<string>(
                name: "FStation",
                table: "TrialEntry",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 560, DateTimeKind.Local).AddTicks(1753),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 400, DateTimeKind.Local).AddTicks(4259));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 559, DateTimeKind.Local).AddTicks(1745),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 399, DateTimeKind.Local).AddTicks(4007));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 390, DateTimeKind.Local).AddTicks(4004),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 549, DateTimeKind.Local).AddTicks(1772));

            migrationBuilder.AlterColumn<string>(
                name: "FStation",
                table: "TrialEntry",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 400, DateTimeKind.Local).AddTicks(4259),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 560, DateTimeKind.Local).AddTicks(1753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 51, 43, 399, DateTimeKind.Local).AddTicks(4007),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 52, 11, 559, DateTimeKind.Local).AddTicks(1745));
        }
    }
}
