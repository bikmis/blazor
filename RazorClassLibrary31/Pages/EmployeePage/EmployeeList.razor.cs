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
        public IEmployeeService EmployeeService { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>(); //Assign an empty object or check null in the razor to avoid an exception.

       // public EmployeeEdit EmployeeEdit;

        protected async override Task OnInitializedAsync()
        {
            await GetEmployees();
        }

        public async void DeleteEmployee(int employeeId)
        {
            await EmployeeService.DeleteEmployee(employeeId);
            await RefreshEmployees();
        }

        public async Task RefreshEmployees() {
            await GetEmployees();
            StateHasChanged();
        }

        public async Task GetEmployees() {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }

        public void PassToEditForm(Employee employee) {
         /*   EmployeeEdit.Employee = new Employee()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position
            };
         */
        }
    }
}
