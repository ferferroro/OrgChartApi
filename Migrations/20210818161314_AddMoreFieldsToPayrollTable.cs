using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrgChartApi.Migrations
{
    public partial class AddMoreFieldsToPayrollTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CutOffDay1",
                table: "Payroll",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CutOffDay2",
                table: "Payroll",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CutOffDay3",
                table: "Payroll",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CutOffDay4",
                table: "Payroll",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PayrollDay1",
                table: "Payroll",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PayrollDay2",
                table: "Payroll",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CutOffDay1",
                table: "Payroll");

            migrationBuilder.DropColumn(
                name: "CutOffDay2",
                table: "Payroll");

            migrationBuilder.DropColumn(
                name: "CutOffDay3",
                table: "Payroll");

            migrationBuilder.DropColumn(
                name: "CutOffDay4",
                table: "Payroll");

            migrationBuilder.DropColumn(
                name: "PayrollDay1",
                table: "Payroll");

            migrationBuilder.DropColumn(
                name: "PayrollDay2",
                table: "Payroll");
        }
    }
}
