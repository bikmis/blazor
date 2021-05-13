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

        private async void addEmployee(EditContext editContext)
        {
            await employeeService.AddEmployee(employee);
            var message = "Employee Added Successfully.";
            navigationManager.NavigateTo($"/employeelist/{message}");
        }

        private void resetForm() {
            employee = new Employee();
        }
    }
}
