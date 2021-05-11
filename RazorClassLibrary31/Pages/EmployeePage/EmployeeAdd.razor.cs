using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.EmployeeService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.EmployeePage
{
    public partial class EmployeeAdd : ComponentBase
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IEmployeeService employeeService { get; set; }
        private Employee employee { get; set; } = new Employee() { FirstName = "", LastName = "", MiddleName = "", Position = "", DateOfBirth = null };

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        public async void AddEmployee(EditContext editContext)
        {
            await employeeService.AddEmployee(employee);
            navigationManager.NavigateTo("/employeelist");
        }
    }
}
