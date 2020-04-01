using Microsoft.EntityFrameworkCore.Migrations;

namespace Astro.DAL.Migrations
{
    public partial class InsightEndDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "148b4ef8-1051-45e1-83a6-445e7100c3fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b33de29-5075-4696-a224-7a5d5686553e");

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Insights",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dea0783d-a19e-4346-a387-3f5fccc16abc", "4374cf45-3c23-4704-a861-665a9b4696c0", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37609623-0323-4a8c-aefd-ece8a584f3df", "22eac896-d8bf-414d-b120-47de6290e12d", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37609623-0323-4a8c-aefd-ece8a584f3df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dea0783d-a19e-4346-a387-3f5fccc16abc");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Insights");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6b33de29-5075-4696-a224-7a5d5686553e", "cd108f1c-bd60-4578-a69b-b08561a70f65", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "148b4ef8-1051-45e1-83a6-445e7100c3fe", "51bba7a3-8b37-4017-8543-9ad7ca2862f8", "User", "USER" });
        }
    }
}
