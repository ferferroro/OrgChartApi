using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrgChartApi.Migrations
{
    public partial class AddTeamAndSubTeamEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CalendarId = table.Column<long>(type: "bigint", nullable: false),
                    PayrollId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStatusTemplateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubTeam",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeamId = table.Column<long>(type: "bigint", nullable: true),
                    SubTeamId = table.Column<long>(type: "bigint", nullable: true),
                    CalendarId = table.Column<long>(type: "bigint", nullable: false),
                    PayrollId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStatusTemplateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTeam_SubTeam_SubTeamId",
                        column: x => x.SubTeamId,
                        principalTable: "SubTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SubTeam_SubTeamId",
                table: "SubTeam",
                column: "SubTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTeam_TeamId",
                table: "SubTeam",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTeam");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
