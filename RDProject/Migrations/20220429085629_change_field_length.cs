using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDProject.Migrations
{
    public partial class change_field_length : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubBy",
                table: "WFStep",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "WFStep",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookMark",
                table: "WFStep",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "WFInstance",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 288, DateTimeKind.Local).AddTicks(26),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 558, DateTimeKind.Local).AddTicks(162));

            migrationBuilder.AlterColumn<string>(
                name: "SubBy",
                table: "WFInstance",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstanceGuid",
                table: "WFInstance",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextName",
                table: "WFInstance",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FWorkOrder",
                table: "TrialEntry",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FProcessName",
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
                defaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 304, DateTimeKind.Local).AddTicks(74),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 567, DateTimeKind.Local).AddTicks(455));

            migrationBuilder.AlterColumn<string>(
                name: "FWorkerOrderDescription",
                table: "Trial",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FTitle",
                table: "Trial",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FRequire",
                table: "Trial",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FRDNo",
                table: "Trial",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FProductName",
                table: "Trial",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FLaserNPI",
                table: "Trial",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FInformation",
                table: "Trial",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FCreateUser",
                table: "Trial",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 303, DateTimeKind.Local).AddTicks(40),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 567, DateTimeKind.Local).AddTicks(455));

            migrationBuilder.AlterColumn<string>(
                name: "FCompany",
                table: "Trial",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FCoatingNPI",
                table: "Trial",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FCNCNPI",
                table: "Trial",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FBillNo",
                table: "Trial",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FAssemblyNPI",
                table: "Trial",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FAssemblyFactory",
                table: "Trial",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employee",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "EmpName",
                table: "Employee",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextName",
                table: "WFInstance");

            migrationBuilder.AlterColumn<string>(
                name: "SubBy",
                table: "WFStep",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "WFStep",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookMark",
                table: "WFStep",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "WFInstance",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubTime",
                table: "WFInstance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 558, DateTimeKind.Local).AddTicks(162),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 288, DateTimeKind.Local).AddTicks(26));

            migrationBuilder.AlterColumn<string>(
                name: "SubBy",
                table: "WFInstance",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstanceGuid",
                table: "WFInstance",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FWorkOrder",
                table: "TrialEntry",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FProcessName",
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
                defaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 567, DateTimeKind.Local).AddTicks(455),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 304, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.AlterColumn<string>(
                name: "FWorkerOrderDescription",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FTitle",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FRequire",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FRDNo",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FProductName",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FLaserNPI",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FInformation",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FCreateUser",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FCreateDate",
                table: "Trial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 21, 15, 45, 11, 567, DateTimeKind.Local).AddTicks(455),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 4, 29, 16, 56, 29, 303, DateTimeKind.Local).AddTicks(40));

            migrationBuilder.AlterColumn<string>(
                name: "FCompany",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FCoatingNPI",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FCNCNPI",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FBillNo",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FAssemblyNPI",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FAssemblyFactory",
                table: "Trial",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employee",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "EmpName",
                table: "Employee",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16);
        }
    }
}
