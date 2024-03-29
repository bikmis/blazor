﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intel.EmployeeManagement.IdentityProvider.Migrations
{
    public partial class data_mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DepartmentID", "Gender" },
                values: new object[] { 5, "M" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "DepartmentID", "Gender" },
                values: new object[] { 10, "M" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "DateOfBirth", "DepartmentID", "FirstName", "Gender", "LastName", "MiddleName", "Position" },
                values: new object[,]
                {
                    { 3, new DateTime(1987, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Jane", "F", "Ferguson", "Williams", "Operations Manager" },
                    { 4, new DateTime(1978, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Leo", "M", "Wilson", "", "Head Chef" },
                    { 5, new DateTime(1974, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Harry", "M", "Burton", "", "CEO" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "RoleName", "UserID" },
                values: new object[,]
                {
                    { 1, "hr", 1 },
                    { 2, "hr", 2 },
                    { 3, "data entry", 3 },
                    { 4, "admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "bikash@gmail.com", "password", "bikash@gmail.com" },
                    { 2, "jack@gmail.com", "password", "jack@gmail.com" },
                    { 3, "mike@gmail.com", "password", "mike@gmail.com" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DepartmentID", "Gender" },
                values: new object[] { 0, " " });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "DepartmentID", "Gender" },
                values: new object[] { 0, " " });
        }
    }
}
