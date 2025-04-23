using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorshipProgramPlannerApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Worships",
                columns: table => new
                {
                    WorshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorshipDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorshipName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worships", x => x.WorshipId);
                });

            migrationBuilder.CreateTable(
                name: "WorshipPrograms",
                columns: table => new
                {
                    WorshipProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoetryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SongName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorshipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorshipPrograms", x => x.WorshipProgramId);
                    table.ForeignKey(
                        name: "FK_WorshipPrograms_Worships_WorshipId",
                        column: x => x.WorshipId,
                        principalTable: "Worships",
                        principalColumn: "WorshipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorshipPrograms_WorshipId",
                table: "WorshipPrograms",
                column: "WorshipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorshipPrograms");

            migrationBuilder.DropTable(
                name: "Worships");
        }
    }
}
