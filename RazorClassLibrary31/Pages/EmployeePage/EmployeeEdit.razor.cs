using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.EmployeeService;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.EmployeePage
{
    public partial class EmployeeEdit
    {
        [Inject]
        IEmployeeService EmployeeService { get; set; }

        public Employee Employee { get; set; } = new Employee();

        [Parameter]
        public EventCallback OnEmployeeEdited { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        public async Task EditEmployee() {
            await EmployeeService.EditEmployee(Employee);
            await JsRuntime.InvokeVoidAsync("closeEmployeeEditModal");
            var isEdited = true;
            await OnEmployeeEdited.InvokeAsync(isEdited);
        }

    }
}
