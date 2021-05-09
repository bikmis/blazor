using Microsoft.AspNetCore.Components;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.EmployeeService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.EmployeePage
{
    //This is a code-behind file for EmployeeList.razor. It should be a partial class with the same name as the razor page.
    public partial class EmployeeList : ComponentBase
    {
        [Inject]
        private IEmployeeService EmployeeService { get; set; }
        private List<Employee> Employees { get; set; } = new List<Employee>(); //Assign an empty object or check null in the razor to avoid an exception.

        private EmployeeEdit EmployeeEdit;

        protected async override Task OnInitializedAsync()
        {
            await GetEmployees();
        }

        private async void DeleteEmployee(int employeeId)
        {
            await EmployeeService.DeleteEmployee(employeeId);
            await RefreshEmployees("");
        }

        private async Task RefreshEmployees(string message) {
            await GetEmployees();
            StateHasChanged();
        }

        private async Task GetEmployees() {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }

        private void PassToEditForm(Employee employee) {
            EmployeeEdit.Employee = new Employee()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position
            };         
        }
    }
}
