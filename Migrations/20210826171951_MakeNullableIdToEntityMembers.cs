using Microsoft.EntityFrameworkCore.Migrations;

namespace OrgChartApi.Migrations
{
    public partial class MakeNullableIdToEntityMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_Company_CompanyId",
                table: "EntityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_Department_DepartmentId",
                table: "EntityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_Employee_EmployeeId",
                table: "EntityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_SubTeam_SubTeamId",
                table: "EntityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_Team_TeamId",
                table: "EntityMembers");

            migrationBuilder.AlterColumn<long>(
                name: "TeamId",
                table: "EntityMembers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SubTeamId",
                table: "EntityMembers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EmployeeId",
                table: "EntityMembers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "DepartmentId",
                table: "EntityMembers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "EntityMembers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_Company_CompanyId",
                table: "EntityMembers",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_Department_DepartmentId",
                table: "EntityMembers",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_Employee_EmployeeId",
                table: "EntityMembers",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_SubTeam_SubTeamId",
                table: "EntityMembers",
                column: "SubTeamId",
                principalTable: "SubTeam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_Team_TeamId",
                table: "EntityMembers",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_Company_CompanyId",
                table: "EntityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_Department_DepartmentId",
                table: "EntityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_Employee_EmployeeId",
                table: "EntityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_SubTeam_SubTeamId",
                table: "EntityMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityMembers_Team_TeamId",
                table: "EntityMembers");

            migrationBuilder.AlterColumn<long>(
                name: "TeamId",
                table: "EntityMembers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubTeamId",
                table: "EntityMembers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EmployeeId",
                table: "EntityMembers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DepartmentId",
                table: "EntityMembers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "EntityMembers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_Company_CompanyId",
                table: "EntityMembers",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_Department_DepartmentId",
                table: "EntityMembers",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_Employee_EmployeeId",
                table: "EntityMembers",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_SubTeam_SubTeamId",
                table: "EntityMembers",
                column: "SubTeamId",
                principalTable: "SubTeam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityMembers_Team_TeamId",
                table: "EntityMembers",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
