using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage
{
    //This is a code-behind file for EmployeeList.razor. It should be a partial class with the same name as the razor page.
    public partial class EmployeeList
    {
        [Inject]
        private IAppStateService appStateService { get; set; }

        [Inject]
        private IEmployeeService employeeService { get; set; }

        private List<Employee> employees { get; set; }// = new List<Employee>(); //Assign an empty object or check null in the razor to avoid an exception.

        private List<Employee> initialCollectionOfEmployees { get; set; }

        private string searchTerm { get; set; }

        private EmployeeEdit employeeEditComponent;

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Parameter]
        public string SaveMessage { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((IAuthenticationService)authenticationService).GuardRoute();
            appStateService.AlertPopUp = new AlertPopUp() { IsHidden = true }; //when user lands on this page, the alert will be hidden.
            displaySaveMessage(); //When user lands on this page after adding an employee, save message is displayed.
            await getEmployees();
        }

        private void displaySaveMessage()
        {
            //This page has two URLs, one with SaveMessage parameter.
            //@page "/employeelist"
            //@page "/employeelist/{SaveMessage}"            
            if (SaveMessage != null)
            {
                var keyValueDictionary = Utility.ParseUri(navigationManager.Uri);
                var alertColor = keyValueDictionary.Where(x => x.Key == "alertColor").FirstOrDefault().Value;

                appStateService.AlertPopUp = new AlertPopUp() { Message = SaveMessage, IsHidden = false, Color = alertColor };
            }
        }

        private void search()
        {
            if (initialCollectionOfEmployees == null)
            {
                initialCollectionOfEmployees = new List<Employee>();
                employees.ForEach(e => initialCollectionOfEmployees.Add(e));
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                employees = initialCollectionOfEmployees.Where(e => (e.ID + e.FirstName + e.MiddleName + e.LastName + Convert.ToDateTime(e.DateOfBirth).ToShortDateString() + e.Position + +e.DepartmentID + e.Gender).ToLower().Contains(searchTerm.ToLower())).ToList();
                if (employees.Count == 0)
                {
                    employees = new List<Employee>();
                }
            }
            else
            {
                employees = new List<Employee>();
                initialCollectionOfEmployees.ForEach(e => employees.Add(e));
            }
        }

        private async Task deleteEmployee(int employeeId)
        {
            try
            {
                var response = await employeeService.DeleteEmployee(employeeId);
                if (response.IsSuccessStatusCode)
                {
                    await reloadEmployees(true);
                    appStateService.AlertPopUp = new AlertPopUp() { Message = $"Employee with ID {employeeId} is deleted.", IsHidden = false, Color = "alert-success" };
                }
                else
                {
                    appStateService.AlertPopUp = new AlertPopUp() { Message = $"{(int)response.StatusCode} {response.ReasonPhrase}", IsHidden = false, Color = "alert-danger" };
                }
            }
            catch (Exception e)
            {
                appStateService.AlertPopUp = new AlertPopUp() { Message = e.Message, IsHidden = false, Color = "alert-danger" };
            }
        }

        private async Task reloadEmployees(bool isOperationCompleted)
        {
            if (isOperationCompleted) {
                await getEmployees();
                StateHasChanged();
            }
        }

        private async Task getEmployees()
        {
            try
            {
                var response = await employeeService.GetEmployees();
                if (response.IsSuccessStatusCode)
                {
                    employees = (await appStateService.DeserializeToList<Employee>(response)).ToList();
                }
                else {
                    appStateService.AlertPopUp = new AlertPopUp() { Message = $"{(int)response.StatusCode} {response.ReasonPhrase}", IsHidden = false, Color = "alert-danger" };
                }
            }
            catch (Exception e)
            {
                appStateService.AlertPopUp = new AlertPopUp() { Message = e.Message, IsHidden = false, Color = "alert-danger" };
            }
        }

        private void passToEditForm(Employee employee)
        {
            employeeEditComponent.Employee = new Employee()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position,
                DepartmentID = employee.DepartmentID,
                Gender = employee.Gender
            };

            employeeEditComponent.EmployeeInitialState = new Employee()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position,
                DepartmentID = employee.DepartmentID,
                Gender = employee.Gender
            };
        }
    }
}
