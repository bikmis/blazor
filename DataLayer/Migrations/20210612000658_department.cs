using Microsoft.EntityFrameworkCore.Migrations;

namespace Intel.EmployeeManagement.Data.Migrations
{
    public partial class department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Employees",
                newName: "DepartmentNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentNumber",
                table: "Employees",
                newName: "Age");
        }
    }
}
