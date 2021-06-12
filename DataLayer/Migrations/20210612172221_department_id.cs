using Microsoft.EntityFrameworkCore.Migrations;

namespace Intel.EmployeeManagement.Data.Migrations
{
    public partial class department_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentNumber",
                table: "Employees",
                newName: "DepartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "Employees",
                newName: "DepartmentNumber");
        }
    }
}
