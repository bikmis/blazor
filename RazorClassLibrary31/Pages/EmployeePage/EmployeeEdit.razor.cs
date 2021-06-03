using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage
{
    public partial class EmployeeEdit
    {
        [Inject]
        private IEmployeeService employeeService { get; set; }

        public Employee Employee { get; set; } = new Employee(); //Initialized from the Edit button in the parent form

        public Employee EmployeeInitialState { get; set; }  //Initialized from the Edit button in the parent form

        [Parameter]
        public EventCallback<string> OnEmployeeEdited { get; set; }

        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private async Task editEmployee() {
            await employeeService.EditEmployee(Employee);
            await jsRuntime.InvokeVoidAsync("closeEmployeeEditModal");
            var editMessage = "Successfully edited.";
            await OnEmployeeEdited.InvokeAsync(editMessage);
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
                Age = EmployeeInitialState.Age
            };
        }

    }
}
