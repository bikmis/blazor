using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer31.Migrations
{
    public partial class dateofbirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(1980, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(1985, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2021, 4, 24, 18, 17, 21, 870, DateTimeKind.Local).AddTicks(6878));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2021, 4, 24, 18, 17, 21, 872, DateTimeKind.Local).AddTicks(8892));
        }
    }
}
