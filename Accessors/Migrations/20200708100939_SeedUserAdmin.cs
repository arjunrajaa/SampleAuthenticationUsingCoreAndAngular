using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accessors.Migrations
{
    public partial class SeedUserAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Uid", "DateOfBirth", "FirstName", "LastName", "Password", "RoleId", "UserName" },
                values: new object[] { 1, new DateTime(2020, 7, 8, 10, 9, 38, 33, DateTimeKind.Utc).AddTicks(5722), "Admin", "Admin", "Admin@1234", 1, "admin@quaero.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 1);
        }
    }
}
