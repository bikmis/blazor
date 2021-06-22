using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage
{
    public partial class EmployeeAdd
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IEmployeeService employeeService { get; set; }

        private Employee employee { get; set; } = new Employee() { DepartmentID = 0 };

        private Dictionary<int, string> departments = new Dictionary<int, string>() { { 1, "Marketing" }, { 5, "Operations" }, { 10, "Sales" }, { 15, "Research" }, { 20, "IT" } };

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        private string male = "M";

        private string female = "F";

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
        }

        private async void addEmployee(EditContext editContext, Employee employee)
        {
            string message;
            string alertColor;

            try
            {
                var response = await employeeService.AddEmployee(employee);
                if (response.IsSuccessStatusCode)
                {
                    message = "Employee Added Successfully.";
                    alertColor = "alert-success";
                }
                else
                {
                    message = "Failed to Add Employee.";
                    alertColor = "alert-danger";
                }
                //passing values in the url parameter and query string
                navigationManager.NavigateTo($"/employeelist/{message}?alertColor={alertColor}");
            }
            catch (Exception e)
            {
                message = e.Message;
                alertColor = "alert-danger";
                navigationManager.NavigateTo($"/employeelist/{message}?alertColor={alertColor}");
            }
        }

        private void resetForm() {
            employee = new Employee();
        }
    }
}
