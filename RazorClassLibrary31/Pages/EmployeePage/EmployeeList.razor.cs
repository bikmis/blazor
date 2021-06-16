using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;

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

        private EmployeeEdit employeeEdit;

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Parameter]
        public string SaveMessage { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
            appStateService.AlertPopUp = new AlertPopUp() { IsHidden = true }; //when user lands on this page, the alert will be hidden.
            await getEmployees();
        }

        private Dictionary<string, string> parseUri()
        {
            Dictionary<string, string> queryStringDictionary = new Dictionary<string, string>();
            var uri = navigationManager.Uri;
            var uriSplit = uri.Split('?');
            if (uriSplit.Length == 2)
            {
                var queryString = uriSplit[1];
                var decodedQueryString = WebUtility.UrlDecode(queryString);
                var decodedQueryStringSplit = decodedQueryString.Split('&').ToList();
                decodedQueryStringSplit.ForEach(x =>
                {
                    var xSplit = x.Split('=');
                    queryStringDictionary.Add(xSplit[0], xSplit[1]);
                });
            }

            return queryStringDictionary;
        }

        protected override Task OnParametersSetAsync()
        {
            //This page has two URLs, one with SaveMessage parameter. This is captured inside OnParametersSetAsync()
            //@page "/employeelist"
            //@page "/employeelist/{SaveMessage}"

            if (SaveMessage != null)
            {
                var queryString = parseUri();
                var alertColor = queryString.Where(x => x.Key == "alertColor").FirstOrDefault().Value;

                appStateService.AlertPopUp = new AlertPopUp() { Message = SaveMessage, IsHidden = false, Color = alertColor };
            }
            return base.OnParametersSetAsync();
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
                employees = (await employeeService.GetEmployees()).ToList();
            }
            catch (Exception e)
            {
                appStateService.AlertPopUp = new AlertPopUp() { Message = e.Message, IsHidden = false, Color = "alert-danger" };
            }
        }

        private void passToEditForm(Employee employee)
        {
            employeeEdit.Employee = new Employee()
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

            employeeEdit.EmployeeInitialState = new Employee()
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
