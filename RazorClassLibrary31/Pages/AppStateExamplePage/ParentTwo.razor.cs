using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.AppStateExamplePage
{
    public partial class ParentTwo
    {
        [Inject]
        private IAppStateService appStateService { get; set; }

        [CascadingParameter]
        private CascadingAppState cascadingAppState { get; set; }

        protected override Task OnInitializedAsync()
        {
            appStateService.OnChange += StateHasChanged;
            return base.OnInitializedAsync();
        }

        private void setTime()
        {
            appStateService.Time = DateTime.Now;
        }

    }
}
