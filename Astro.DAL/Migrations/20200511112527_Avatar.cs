using Microsoft.EntityFrameworkCore.Migrations;

namespace Astro.DAL.Migrations
{
    public partial class Avatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37609623-0323-4a8c-aefd-ece8a584f3df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dea0783d-a19e-4346-a387-3f5fccc16abc");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dea0783d-a19e-4346-a387-3f5fccc16abc", "4374cf45-3c23-4704-a861-665a9b4696c0", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37609623-0323-4a8c-aefd-ece8a584f3df", "22eac896-d8bf-414d-b120-47de6290e12d", "User", "USER" });
        }
    }
}
