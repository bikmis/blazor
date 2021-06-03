using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intel.Personnel.Data.Migrations
{
    public partial class more_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2021, 4, 23, 21, 14, 23, 617, DateTimeKind.Local).AddTicks(2495));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "DateOfBirth", "FirstName", "LastName", "MiddleName", "Position" },
                values: new object[] { 2, new DateTime(2021, 4, 23, 21, 14, 23, 619, DateTimeKind.Local).AddTicks(6501), "Jack", "Jackson", "", "Operations Manager" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2021, 4, 23, 17, 6, 38, 353, DateTimeKind.Local).AddTicks(742));
        }
    }
}
