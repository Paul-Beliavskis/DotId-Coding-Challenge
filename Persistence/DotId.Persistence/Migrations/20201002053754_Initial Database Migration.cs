using Microsoft.EntityFrameworkCore.Migrations;

namespace DotId.Persistence.Migrations
{
    public partial class InitialDatabaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(nullable: true),
                    Median = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(nullable: true),
                    PlaceName = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Locations_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ScoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisadvantageScore = table.Column<int>(nullable: true),
                    AdvantageDisadvantageScore = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ScoreId);
                    table.ForeignKey(
                        name: "FK_Scores_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoreDetails",
                columns: table => new
                {
                    ScoreDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoreId = table.Column<int>(nullable: true),
                    AdvantageDisadvantageDecile = table.Column<int>(nullable: true),
                    DisadvantageDecile = table.Column<int>(nullable: true),
                    IndexOfEconomicResourcesScore = table.Column<int>(nullable: true),
                    IndexOfEconomicResourcesDecile = table.Column<int>(nullable: true),
                    IndexOfEducationAndOccupationScore = table.Column<int>(nullable: true),
                    IndexOfEducationAndOccupationDecile = table.Column<int>(nullable: true),
                    UsualResedantPopulation = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreDetails", x => x.ScoreDetailsId);
                    table.ForeignKey(
                        name: "FK_ScoreDetails_Scores_ScoreId",
                        column: x => x.ScoreId,
                        principalTable: "Scores",
                        principalColumn: "ScoreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_StateId",
                table: "Locations",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreDetails_ScoreId",
                table: "ScoreDetails",
                column: "ScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_LocationId",
                table: "Scores",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreDetails");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
