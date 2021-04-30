using EmployeeBlazor.Models;
using EmployeeBlazor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBlazor.Pages.EmployeePage 
{
    public partial class EmployeeAdd
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public Employee Employee { get; set; }

        protected override Task OnInitializedAsync()
        {
            Employee = new Employee();
            return base.OnInitializedAsync();
        }

        public async void AddEmployee(Employee employee) {
            await EmployeeService.AddEmployee(employee);
            NavigationManager.NavigateTo("/employeelist");
        }
    }
}
