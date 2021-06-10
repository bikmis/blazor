using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using System.Threading.Tasks;
using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage
{
    public partial class EmployeeAdd
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IEmployeeService employeeService { get; set; }

        private Employee employee { get; set; } = new Employee();

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

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
                    alertColor = "alert-secondary";
                }
                else
                {
                    message = "Failed to Add Employee.";
                    alertColor = "alert-danger";
                }
                //passing values in the url parameter and query string
                navigationManager.NavigateTo($"/employeelist/{message}?alertColor={alertColor}&messageOne=message one&messageTwo=message two&messageThree=message three");
            }
            catch (Exception e)
            {
                message = e.Message;
                alertColor = "alert-danger";
                navigationManager.NavigateTo($"/employeelist/{message}?alertColor={alertColor}&messageOne=message one&messageTwo=message two&messageThree=message three");
            }
        }

        private void resetForm() {
            employee = new Employee();
        }
    }
}
