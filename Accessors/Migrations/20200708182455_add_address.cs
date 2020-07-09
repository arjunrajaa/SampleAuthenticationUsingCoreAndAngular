using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accessors.Migrations
{
    public partial class add_address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 7, 8, 18, 24, 54, 248, DateTimeKind.Utc).AddTicks(5497));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2020, 7, 8, 10, 9, 38, 33, DateTimeKind.Utc).AddTicks(5722));
        }
    }
}
