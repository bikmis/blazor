using EmployeeBlazor.Models;
using EmployeeBlazor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBlazor.Pages
{
    public partial class EmployeeList
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public List<Employee> Employees { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await InitializeEmployeeList();
            //await base.OnInitializedAsync();
        }

        private async Task InitializeEmployeeList()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }

    }
}
