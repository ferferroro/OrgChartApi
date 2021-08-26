using Microsoft.EntityFrameworkCore.Migrations;

namespace OrgChartApi.Migrations
{
    public partial class AddHeadToEntityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Team",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "Team",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Team",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "Team",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "SubTeam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "SubTeam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "SubTeam",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "SubTeam",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Employee",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "Employee",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Employee",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "Employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Department",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "Department",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Department",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "Department",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Company",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "Company",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Company",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "Company",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_CalendarId",
                table: "Team",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_EmployeeId",
                table: "Team",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_PayrollId",
                table: "Team",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_WorkStatusTemplateId",
                table: "Team",
                column: "WorkStatusTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTeam_CalendarId",
                table: "SubTeam",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTeam_EmployeeId",
                table: "SubTeam",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTeam_PayrollId",
                table: "SubTeam",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTeam_WorkStatusTemplateId",
                table: "SubTeam",
                column: "WorkStatusTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CalendarId",
                table: "Employee",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeId",
                table: "Employee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PayrollId",
                table: "Employee",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_WorkStatusTemplateId",
                table: "Employee",
                column: "WorkStatusTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CalendarId",
                table: "Department",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_EmployeeId",
                table: "Department",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_PayrollId",
                table: "Department",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_WorkStatusTemplateId",
                table: "Department",
                column: "WorkStatusTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CalendarId",
                table: "Company",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_EmployeeId",
                table: "Company",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_PayrollId",
                table: "Company",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_WorkStatusTemplateId",
                table: "Company",
                column: "WorkStatusTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Calendar_CalendarId",
                table: "Company",
                column: "CalendarId",
                principalTable: "Calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Employee_EmployeeId",
                table: "Company",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Payroll_PayrollId",
                table: "Company",
                column: "PayrollId",
                principalTable: "Payroll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_WorkStatusTemplate_WorkStatusTemplateId",
                table: "Company",
                column: "WorkStatusTemplateId",
                principalTable: "WorkStatusTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Calendar_CalendarId",
                table: "Department",
                column: "CalendarId",
                principalTable: "Calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Employee_EmployeeId",
                table: "Department",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Payroll_PayrollId",
                table: "Department",
                column: "PayrollId",
                principalTable: "Payroll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_WorkStatusTemplate_WorkStatusTemplateId",
                table: "Department",
                column: "WorkStatusTemplateId",
                principalTable: "WorkStatusTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Calendar_CalendarId",
                table: "Employee",
                column: "CalendarId",
                principalTable: "Calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Employee_EmployeeId",
                table: "Employee",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Payroll_PayrollId",
                table: "Employee",
                column: "PayrollId",
                principalTable: "Payroll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_WorkStatusTemplate_WorkStatusTemplateId",
                table: "Employee",
                column: "WorkStatusTemplateId",
                principalTable: "WorkStatusTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTeam_Calendar_CalendarId",
                table: "SubTeam",
                column: "CalendarId",
                principalTable: "Calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTeam_Employee_EmployeeId",
                table: "SubTeam",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTeam_Payroll_PayrollId",
                table: "SubTeam",
                column: "PayrollId",
                principalTable: "Payroll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTeam_WorkStatusTemplate_WorkStatusTemplateId",
                table: "SubTeam",
                column: "WorkStatusTemplateId",
                principalTable: "WorkStatusTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Calendar_CalendarId",
                table: "Team",
                column: "CalendarId",
                principalTable: "Calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Employee_EmployeeId",
                table: "Team",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Payroll_PayrollId",
                table: "Team",
                column: "PayrollId",
                principalTable: "Payroll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_WorkStatusTemplate_WorkStatusTemplateId",
                table: "Team",
                column: "WorkStatusTemplateId",
                principalTable: "WorkStatusTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Calendar_CalendarId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Employee_EmployeeId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Payroll_PayrollId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_WorkStatusTemplate_WorkStatusTemplateId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Calendar_CalendarId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Employee_EmployeeId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Payroll_PayrollId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_WorkStatusTemplate_WorkStatusTemplateId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Calendar_CalendarId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Employee_EmployeeId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Payroll_PayrollId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_WorkStatusTemplate_WorkStatusTemplateId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTeam_Calendar_CalendarId",
                table: "SubTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTeam_Employee_EmployeeId",
                table: "SubTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTeam_Payroll_PayrollId",
                table: "SubTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTeam_WorkStatusTemplate_WorkStatusTemplateId",
                table: "SubTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Calendar_CalendarId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Employee_EmployeeId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Payroll_PayrollId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_WorkStatusTemplate_WorkStatusTemplateId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_CalendarId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_EmployeeId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_PayrollId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_WorkStatusTemplateId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_SubTeam_CalendarId",
                table: "SubTeam");

            migrationBuilder.DropIndex(
                name: "IX_SubTeam_EmployeeId",
                table: "SubTeam");

            migrationBuilder.DropIndex(
                name: "IX_SubTeam_PayrollId",
                table: "SubTeam");

            migrationBuilder.DropIndex(
                name: "IX_SubTeam_WorkStatusTemplateId",
                table: "SubTeam");

            migrationBuilder.DropIndex(
                name: "IX_Employee_CalendarId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_EmployeeId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_PayrollId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_WorkStatusTemplateId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Department_CalendarId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_EmployeeId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_PayrollId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_WorkStatusTemplateId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Company_CalendarId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_EmployeeId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_PayrollId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_WorkStatusTemplateId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "SubTeam");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Company");

            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Team",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "Team",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Team",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "SubTeam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "SubTeam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "SubTeam",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Department",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "Department",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Department",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WorkStatusTemplateId",
                table: "Company",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PayrollId",
                table: "Company",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Company",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
