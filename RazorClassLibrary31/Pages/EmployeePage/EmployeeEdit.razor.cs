using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage
{
    public partial class EmployeeEdit
    {
        [Inject]
        private IEmployeeService employeeService { get; set; }

        public Employee Employee { get; set; } = new Employee(); //Initialized from the Edit button in the parent form

        private Dictionary<int, string> departments = new Dictionary<int, string>() { { 1, "Marketing" }, { 5, "Operations" }, { 10, "Sales" }, { 15, "Research" }, { 20, "IT" } };

        public Employee EmployeeInitialState { get; set; }  //Initialized from the Edit button in the parent form

        [Parameter]
        public EventCallback<AlertPopUp> OnEmployeeEdited { get; set; }

        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private async Task editEmployee()
        {
            AlertPopUp alertPopUp = new AlertPopUp() { IsHidden = false };

            try
            {
                var response = await employeeService.EditEmployee(Employee);
                await jsRuntime.InvokeVoidAsync("closeEmployeeEditModal");

                if (response.IsSuccessStatusCode)
                {
                    alertPopUp.Message = "Successfully edited.";
                    alertPopUp.Color = "alert-primary";
                }
                else
                {
                    alertPopUp.Message = $"{(int)response.StatusCode} {response.StatusCode}";
                    alertPopUp.Color = "alert-danger";
                }
                await OnEmployeeEdited.InvokeAsync(alertPopUp);
            }
            catch (Exception e)
            {
                await jsRuntime.InvokeVoidAsync("closeEmployeeEditModal");
                alertPopUp.Message = e.Message;
                alertPopUp.Color = "alert-danger";
                await OnEmployeeEdited.InvokeAsync(alertPopUp);
            }
        }

        private void resetForm() {
            Employee = new Employee()
            {
                ID = EmployeeInitialState.ID,
                FirstName = EmployeeInitialState.FirstName,
                MiddleName = EmployeeInitialState.MiddleName,
                LastName = EmployeeInitialState.LastName,
                DateOfBirth = EmployeeInitialState.DateOfBirth,
                Position = EmployeeInitialState.Position,
                DepartmentID = EmployeeInitialState.DepartmentID,
                Gender = EmployeeInitialState.Gender
            };
        }

    }
}
