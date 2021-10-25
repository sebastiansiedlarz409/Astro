using Microsoft.EntityFrameworkCore.Migrations;

namespace Astro.DAL.Migrations
{
    public partial class RemoveInsight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insights");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Insights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgPress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvgTemp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvgWind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxTemp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxWind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinPress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinTemp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinWind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insights", x => x.Id);
                });
        }
    }
}
