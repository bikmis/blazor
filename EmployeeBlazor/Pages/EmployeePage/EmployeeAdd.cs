﻿using EmployeeBlazor.Models;
using EmployeeBlazor.Services.EmployeeService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
        public Employee Employee { get; set; } = new Employee() { FirstName = "", LastName = "", MiddleName = "", Position = "", DateOfBirth = null };

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        public async void AddEmployee(EditContext editContext) {
            await EmployeeService.AddEmployee(Employee);
            NavigationManager.NavigateTo("/employeelist");
        }
    }
}
