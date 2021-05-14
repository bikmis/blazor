using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.EmployeeService;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.EmployeePage
{
    public partial class EmployeeAdd : ComponentBase
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IEmployeeService employeeService { get; set; }

        private Employee employee { get; set; } = new Employee();

        protected override Task OnInitializedAsync()
        {            
            return base.OnInitializedAsync();
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
