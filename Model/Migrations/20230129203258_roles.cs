using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektTNAI.Model.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7311f4ed-b6b6-40ca-a504-0dfe9c98e293", "11cb0eb7-3ff9-408f-af65-43190731f642", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7342c908-a6a0-474b-88d8-57e15f52e47a", "9b56837a-687a-4f10-a342-311ebc59c12f", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7311f4ed-b6b6-40ca-a504-0dfe9c98e293");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7342c908-a6a0-474b-88d8-57e15f52e47a");
        }
    }
}
