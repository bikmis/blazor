using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Razor.Components.Library.Models;
using Razor.Components.Library.Services.EmployeeService;
using System.Threading.Tasks;

namespace Razor.Components.Library.Pages.EmployeePage
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
            await OnEmployeeEdited.InvokeAsync();
        }

    }
}
