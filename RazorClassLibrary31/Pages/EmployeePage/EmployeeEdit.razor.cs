using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage
{
    public partial class EmployeeEdit
    {
        [Inject]
        private IAppStateService appStateService { get; set; }

        private string male = "M";

        private string female = "F";

        [Inject]
        private IEmployeeService employeeService { get; set; }

        public Employee Employee { get; set; } = new Employee(); //Initialized from the Edit button in the parent form

        private Dictionary<int, string> departments = new Dictionary<int, string>() { { 1, "Marketing" }, { 5, "Operations" }, { 10, "Sales" }, { 15, "Research" }, { 20, "IT" } };

        public Employee EmployeeInitialState { get; set; }  //Initialized from the Edit button in the parent form

        [Parameter]
        public EventCallback<bool> OnEmployeeEdited { get; set; }

        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private async Task editEmployee()
        {
            bool editCompleted = true;
            try
            {
                var response = await employeeService.EditEmployee(Employee);
                await jsRuntime.InvokeVoidAsync("closeEmployeeEditModal");

                if (response.IsSuccessStatusCode)
                {
                    appStateService.AlertPopUp = new AlertPopUp { Message = "Successfully edited.", IsHidden = false, Color = "alert-success" };
                }
                else
                {
                    appStateService.AlertPopUp = new AlertPopUp { Message = $"{(int)response.StatusCode} {response.StatusCode}", IsHidden = false, Color = "alert-danger" };
                }
                await OnEmployeeEdited.InvokeAsync(editCompleted);
            }
            catch (Exception e)
            {
                await jsRuntime.InvokeVoidAsync("closeEmployeeEditModal");
                appStateService.AlertPopUp = new AlertPopUp { Message = e.Message, IsHidden = false, Color = "alert-danger" };
                await OnEmployeeEdited.InvokeAsync(editCompleted);
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
