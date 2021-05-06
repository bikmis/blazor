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
        public string Display { get; set; }
        public string Show { get; set; }
        public string AriaHidden { get; set; }
        public string PaddingRight { get; set; }
        [Parameter]
        public EventCallback OnEmployeeEdited { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        public void DisplayEditForm(Employee employee) {
            Employee = employee;
            Display = "block";
            Show = "show";
            AriaHidden = "false";
            PaddingRight = "17px";
            StateHasChanged();
        }

        public void CloseEditForm() {
            Display = "none";
            Show = "";
            AriaHidden = "true";
            PaddingRight = "0px";
        }

        public async Task EditEmployee() {
            await EmployeeService.EditEmployee(Employee);
            CloseEditForm();
            await OnEmployeeEdited.InvokeAsync();
        }

    }
}
