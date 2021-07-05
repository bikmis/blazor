using Microsoft.EntityFrameworkCore.Migrations;

namespace Intel.EmployeeManagement.IdentityProvider.Migrations
{
    public partial class data_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 3,
                column: "MiddleName",
                value: "Willa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: 3,
                column: "MiddleName",
                value: "Williams");
        }
    }
}
