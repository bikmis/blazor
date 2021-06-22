using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.LogExamplePage
{
    public partial class LogExample
    {
        //Since [Inject] is commented out, it will be written to the Logs table in the database with a log level of critical.

        // [Inject]
        private IEmployeeService employeeService { get; set; }         

        private async Task getEmployees() {
            var empoyees = await employeeService.GetEmployees();
        }



    }
}
