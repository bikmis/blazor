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

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage
{
    //This is a code-behind file for EmployeeList.razor. It should be a partial class with the same name as the razor page.
    public partial class EmployeeList
    {
        [Inject]
        private IEmployeeService employeeService { get; set; }
        private List<Employee> employees { get; set; } = new List<Employee>(); //Assign an empty object or check null in the razor to avoid an exception.

        private EmployeeEdit employeeEdit;

        private string message { get; set; }

        private bool isHidden { get; set; } = true;

        private string alertColor { get; set; }
      
        private void closeMessage(bool isHidden) {
            this.isHidden = isHidden;
        }
      
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Parameter]
        public string SaveMessage { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();

            var queryString = parseUri();
            alertColor = queryString.Where(x => x.Key == "alertColor").FirstOrDefault().Value;
            var messageOne = queryString.Where(x => x.Key == "messageOne").FirstOrDefault().Value;
            var messageTwo = queryString.Where(x => x.Key == "messageTwo").FirstOrDefault().Value;
            var messageThree = queryString.Where(x => x.Key == "messageThree").FirstOrDefault().Value;
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
            if (SaveMessage != null)
            {
                message = SaveMessage;
                isHidden = false;
            }
            return base.OnParametersSetAsync();
        }

        private async Task deleteEmployee(int employeeId)
        {
            try
            {
                var response = await employeeService.DeleteEmployee(employeeId);
                if (response.IsSuccessStatusCode)
                {
                    await refreshEmployees();
                    message = $"Employee with ID {employeeId} is deleted.";
                    isHidden = false;
                    alertColor = "alert-warning";
                }
                else {
                    message = $"{(int)response.StatusCode} {response.ReasonPhrase}";
                    isHidden = false;
                    alertColor = "alert-warning";
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                isHidden = false;
                alertColor = "alert-warning";
            }
        }

        private async Task refreshEmployees() {
            await getEmployees();        
            StateHasChanged();
        }

        private async Task refreshEmployeesAndShowMessage(string message) {
            this.message = message;
            isHidden = false;
            alertColor = "alert-primary";
            await refreshEmployees();
        }

        private async Task getEmployees() {
            try {
                employees = (await employeeService.GetEmployees()).ToList();
            }
            catch (Exception e) {
                //if database is down, employees will be null and the execution will come here as null cannot bind to the view.
                //if api service is down, exception will be caught and rethrown by http service and then by employee service and the execution will come here.
                message = e.Message;
                isHidden = false;
                alertColor = "alert-warning";
            }
        }

        private void passToEditForm(Employee employee) {
            employeeEdit.Employee = new Employee()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position,
                DepartmentNumber = employee.DepartmentNumber
            };

            employeeEdit.EmployeeInitialState = new Employee()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Position = employee.Position,
                DepartmentNumber = employee.DepartmentNumber
            };
        }
    }
}
