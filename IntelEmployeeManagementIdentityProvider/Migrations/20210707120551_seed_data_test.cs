using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intel.EmployeeManagement.IdentityProvider.Migrations
{
    public partial class seed_data_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "DateOfBirth", "DepartmentID", "FirstName", "Gender", "LastName", "MiddleName", "Position" },
                values: new object[] { 6, new DateTime(1991, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Emily", "F", "Taylor", "Rose", "Programmer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 6);
        }
    }
}
