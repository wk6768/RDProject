using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class add_SerialNumber_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 314, DateTimeKind.Local).AddTicks(8082),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 778, DateTimeKind.Local).AddTicks(8366));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 325, DateTimeKind.Local).AddTicks(7791),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 790, DateTimeKind.Local).AddTicks(8362));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 324, DateTimeKind.Local).AddTicks(8105),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 790, DateTimeKind.Local).AddTicks(8362));

            migrationBuilder.CreateTable(
                name: "SerialNumber",
                columns: table => new
                {
                    FId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTableName = table.Column<string>(maxLength: 32, nullable: true),
                    FYear = table.Column<int>(nullable: false),
                    FMonth = table.Column<int>(nullable: false),
                    FNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialNumber", x => x.FId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumber_FTableName",
                table: "SerialNumber",
                column: "FTableName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SerialNumber");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 778, DateTimeKind.Local).AddTicks(8366),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 314, DateTimeKind.Local).AddTicks(8082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 790, DateTimeKind.Local).AddTicks(8362),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 325, DateTimeKind.Local).AddTicks(7791));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 790, DateTimeKind.Local).AddTicks(8362),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 16, 9, 11, 28, 324, DateTimeKind.Local).AddTicks(8105));
        }
    }
}
