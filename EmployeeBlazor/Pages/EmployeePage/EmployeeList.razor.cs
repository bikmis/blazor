﻿using EmployeeBlazor.Models;
using EmployeeBlazor.Services.EmployeeService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBlazor.Pages.EmployeePage
{
    //This is a code-behind file for EmployeeList.razor. It should be a partial class with the same name as the razor page.
    public partial class EmployeeList
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>(); //Assign an empty object or check null in the razor to avoid an exception.

        protected async override Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }

    }
}