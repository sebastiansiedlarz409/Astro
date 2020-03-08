using Microsoft.EntityFrameworkCore.Migrations;

namespace Astro.DAL.Migrations
{
    public partial class AddApiTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bc99f9e-6faa-4b29-96bb-fe41aa235d2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab0c1bee-a0a3-493d-84ec-bcd03d8e2dd3");

            migrationBuilder.CreateTable(
                name: "APOD",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UrlHd = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    MediaType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APOD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AsteroidsNeoWs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Dangerous = table.Column<string>(nullable: true),
                    FirstObservation = table.Column<string>(nullable: true),
                    LastObservation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsteroidsNeoWs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EPIC",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPIC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    MaxTemp = table.Column<string>(nullable: true),
                    AvgTemp = table.Column<string>(nullable: true),
                    MinTemp = table.Column<string>(nullable: true),
                    MaxWind = table.Column<string>(nullable: true),
                    AvgWind = table.Column<string>(nullable: true),
                    MinWind = table.Column<string>(nullable: true),
                    MaxPress = table.Column<string>(nullable: true),
                    AvgPress = table.Column<string>(nullable: true),
                    MinPress = table.Column<string>(nullable: true),
                    Season = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insights", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d33bfe24-d45f-4ce1-b20b-32a80e1729ef", "9ab3b85e-9aeb-4716-9324-7473c19ed2b7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bf28ec92-63e1-4099-b0d0-e99846b83ae0", "8df45cce-38a0-4fe9-9a8e-70a77c4899ae", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APOD");

            migrationBuilder.DropTable(
                name: "AsteroidsNeoWs");

            migrationBuilder.DropTable(
                name: "EPIC");

            migrationBuilder.DropTable(
                name: "Insights");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf28ec92-63e1-4099-b0d0-e99846b83ae0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d33bfe24-d45f-4ce1-b20b-32a80e1729ef");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5bc99f9e-6faa-4b29-96bb-fe41aa235d2c", "c7dc27d4-8806-4021-95c4-c1bf2b6aef90", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab0c1bee-a0a3-493d-84ec-bcd03d8e2dd3", "b253197d-9898-4818-9828-a41e9cdd207e", "User", "USER" });
        }
    }
}
