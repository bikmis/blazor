using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
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

        [Inject]
        private IAppStateService appStateService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((IAuthenticationService)authenticationService).GuardRoute();
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
                    appStateService.AlertPopUp = new AlertPopUp() { Message = "Employee Added Successfully.", IsHidden = false, Color = "alert-success" };
                }
                else
                {
                    appStateService.AlertPopUp = new AlertPopUp() { Message = "Failed to Add Employee.", IsHidden = false, Color = "alert-danger" };
                }
                navigationManager.NavigateTo("/employeelist");
            }
            catch (Exception e)
            {
                appStateService.AlertPopUp = new AlertPopUp() { Message = e.Message, IsHidden = false, Color = "alert-danger" };
                navigationManager.NavigateTo("/employeelist");
            }
        }

        private void resetForm() {
            employee = new Employee();
        }
    }
}
