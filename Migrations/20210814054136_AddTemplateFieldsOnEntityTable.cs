using Microsoft.EntityFrameworkCore.Migrations;

namespace OrgChartApi.Migrations
{
    public partial class AddTemplateFieldsOnEntityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CalendarId",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PayrollId",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CalendarId",
                table: "Department",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PayrollId",
                table: "Department",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Department",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CalendarId",
                table: "Company",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PayrollId",
                table: "Company",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Company",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PayrollId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "WorkStatusTemplateId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "PayrollId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "WorkStatusTemplateId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "PayrollId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "WorkStatusTemplateId",
                table: "Company");
        }
    }
}
