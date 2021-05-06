using Razor.Components.Library.Models;
using Razor.Components.Library.Services.EmployeeService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Razor.Components.Library.Pages.EmployeePage
{
    //This is a code-behind file for EmployeeList.razor. It should be a partial class with the same name as the razor page.
    public partial class EmployeeList
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>(); //Assign an empty object or check null in the razor to avoid an exception.

        public string Display { get; set; } = "";

        public EmployeeEdit EmployeeEdit;

        protected async override Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }

        public async void DeleteEmployee(int employeeId)
        {
            await EmployeeService.DeleteEmployee(employeeId);
            Employees = (await EmployeeService.GetEmployees()).ToList();
            StateHasChanged();
        }

        public void DisplayEditForm() {
            EmployeeEdit.DisplayEditForm();
        }

    }
}
