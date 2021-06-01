using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.Authentication_Service;
using RazorClassLibrary31.Services.Employee_Service;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.EmployeePage
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
            await employeeService.AddEmployee(employee);
            var message = "Employee Added Successfully.";
            var alertColor = "alert-secondary";
            //passing values in the url parameter and query string
            navigationManager.NavigateTo($"/employeelist/{message}?alertColor={alertColor}&messageOne=message one&messageTwo=message two&messageThree=message three");
        }

        private void resetForm() {
            employee = new Employee();
        }
    }
}
