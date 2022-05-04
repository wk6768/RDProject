using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class add_fieldindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 778, DateTimeKind.Local).AddTicks(8366),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 372, DateTimeKind.Local).AddTicks(9192));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 790, DateTimeKind.Local).AddTicks(8362),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 384, DateTimeKind.Local).AddTicks(9189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 790, DateTimeKind.Local).AddTicks(8362),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 383, DateTimeKind.Local).AddTicks(9186));

            migrationBuilder.CreateIndex(
                name: "IX_WFStep_BookMark",
                table: "WFStep",
                column: "BookMark");

            migrationBuilder.CreateIndex(
                name: "IX_WFStep_InstanceId",
                table: "WFStep",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_WFStep_Status",
                table: "WFStep",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WFStep_SubBy",
                table: "WFStep",
                column: "SubBy");

            migrationBuilder.CreateIndex(
                name: "IX_WFInstance_HeadId",
                table: "WFInstance",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_WFInstance_InstanceGuid",
                table: "WFInstance",
                column: "InstanceGuid");

            migrationBuilder.CreateIndex(
                name: "IX_WFInstance_NextName",
                table: "WFInstance",
                column: "NextName");

            migrationBuilder.CreateIndex(
                name: "IX_WFInstance_Status",
                table: "WFInstance",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WFInstance_SubBy",
                table: "WFInstance",
                column: "SubBy");

            migrationBuilder.CreateIndex(
                name: "IX_WFInstance_TableName",
                table: "WFInstance",
                column: "TableName");

            migrationBuilder.CreateIndex(
                name: "IX_WFInstance_Status_NextName",
                table: "WFInstance",
                columns: new[] { "Status", "NextName" });

            migrationBuilder.CreateIndex(
                name: "IX_TrialEntry_FHeadId",
                table: "TrialEntry",
                column: "FHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_TrialEntry_FProcessName",
                table: "TrialEntry",
                column: "FProcessName");

            migrationBuilder.CreateIndex(
                name: "IX_TrialEntry_FWorkOrder",
                table: "TrialEntry",
                column: "FWorkOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Trial_FCompany",
                table: "Trial",
                column: "FCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Trial_FCreateDate",
                table: "Trial",
                column: "FCreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Trial_FCreateUser",
                table: "Trial",
                column: "FCreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_Trial_FDate",
                table: "Trial",
                column: "FDate");

            migrationBuilder.CreateIndex(
                name: "IX_Trial_FProductName",
                table: "Trial",
                column: "FProductName");

            migrationBuilder.CreateIndex(
                name: "IX_Trial_FRDNo",
                table: "Trial",
                column: "FRDNo");

            migrationBuilder.CreateIndex(
                name: "IX_Trial_FStatus",
                table: "Trial",
                column: "FStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Trial_FTitle",
                table: "Trial",
                column: "FTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Name",
                table: "Employee",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WFStep_BookMark",
                table: "WFStep");

            migrationBuilder.DropIndex(
                name: "IX_WFStep_InstanceId",
                table: "WFStep");

            migrationBuilder.DropIndex(
                name: "IX_WFStep_Status",
                table: "WFStep");

            migrationBuilder.DropIndex(
                name: "IX_WFStep_SubBy",
                table: "WFStep");

            migrationBuilder.DropIndex(
                name: "IX_WFInstance_HeadId",
                table: "WFInstance");

            migrationBuilder.DropIndex(
                name: "IX_WFInstance_InstanceGuid",
                table: "WFInstance");

            migrationBuilder.DropIndex(
                name: "IX_WFInstance_NextName",
                table: "WFInstance");

            migrationBuilder.DropIndex(
                name: "IX_WFInstance_Status",
                table: "WFInstance");

            migrationBuilder.DropIndex(
                name: "IX_WFInstance_SubBy",
                table: "WFInstance");

            migrationBuilder.DropIndex(
                name: "IX_WFInstance_TableName",
                table: "WFInstance");

            migrationBuilder.DropIndex(
                name: "IX_WFInstance_Status_NextName",
                table: "WFInstance");

            migrationBuilder.DropIndex(
                name: "IX_TrialEntry_FHeadId",
                table: "TrialEntry");

            migrationBuilder.DropIndex(
                name: "IX_TrialEntry_FProcessName",
                table: "TrialEntry");

            migrationBuilder.DropIndex(
                name: "IX_TrialEntry_FWorkOrder",
                table: "TrialEntry");

            migrationBuilder.DropIndex(
                name: "IX_Trial_FCompany",
                table: "Trial");

            migrationBuilder.DropIndex(
                name: "IX_Trial_FCreateDate",
                table: "Trial");

            migrationBuilder.DropIndex(
                name: "IX_Trial_FCreateUser",
                table: "Trial");

            migrationBuilder.DropIndex(
                name: "IX_Trial_FDate",
                table: "Trial");

            migrationBuilder.DropIndex(
                name: "IX_Trial_FProductName",
                table: "Trial");

            migrationBuilder.DropIndex(
                name: "IX_Trial_FRDNo",
                table: "Trial");

            migrationBuilder.DropIndex(
                name: "IX_Trial_FStatus",
                table: "Trial");

            migrationBuilder.DropIndex(
                name: "IX_Trial_FTitle",
                table: "Trial");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Name",
                table: "Employee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 372, DateTimeKind.Local).AddTicks(9192),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 778, DateTimeKind.Local).AddTicks(8366));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "TrialEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 384, DateTimeKind.Local).AddTicks(9189),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 790, DateTimeKind.Local).AddTicks(8362));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 58, 0, 383, DateTimeKind.Local).AddTicks(9186),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 16, 24, 790, DateTimeKind.Local).AddTicks(8362));
        }
    }
}
