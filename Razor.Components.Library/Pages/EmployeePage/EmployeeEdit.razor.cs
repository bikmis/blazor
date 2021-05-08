using Microsoft.AspNetCore.Components;
using Razor.Components.Library.Models;
using Razor.Components.Library.Services.EmployeeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }


        public void CloseEditForm() {
            // to do
        }

        public async Task EditEmployee() {
            await EmployeeService.EditEmployee(Employee);
            CloseEditForm();
            await OnEmployeeEdited.InvokeAsync();
        }

    }
}
