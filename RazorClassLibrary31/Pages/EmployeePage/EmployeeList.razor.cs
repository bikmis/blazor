﻿using Microsoft.AspNetCore.Components;
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
        private IEmployeeService employeeService { get; set; }
        private List<Employee> employees { get; set; } = new List<Employee>(); //Assign an empty object or check null in the razor to avoid an exception.

        private EmployeeEdit employeeEdit;

        private string employeeEditedMessage { get; set; }
        private bool isHidden { get; set; } = true;

        private void closeMessage() {
            isHidden = true;
        }

        protected async override Task OnInitializedAsync()
        {
            await getEmployees();
        }

        private async Task deleteEmployee(int employeeId)
        {
            await employeeService.DeleteEmployee(employeeId);
            await refreshEmployees();
        }

        private async Task refreshEmployees() {
            await getEmployees();        
            StateHasChanged();
        }

        private async Task refreshEmployeesAndShowMessage(string message) {
            employeeEditedMessage = message;
            isHidden = false;
            await refreshEmployees();
        }

        private async Task getEmployees() {
            employees = (await employeeService.GetEmployees()).ToList();
        }

        private void passToEditForm(Employee employee) {
            employeeEdit.Employee = new Employee()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position
            };

            employeeEdit.EmployeeInitialState = new Employee()
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
