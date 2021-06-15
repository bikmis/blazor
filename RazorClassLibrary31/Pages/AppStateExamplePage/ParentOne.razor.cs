using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.AppStateExamplePage
{
    public partial class ParentOne
    {
        [Inject]
        private IAppStateService appStateService { get; set; }


        protected override Task OnInitializedAsync()
        {
            appStateService.OnChange += StateHasChanged;
            return base.OnInitializedAsync();
        }

        private void setTime() {
            appStateService.Time = DateTime.Now;
        }

    }
}
