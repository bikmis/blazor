using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.AppStateExamplePage
{
    public partial class ParentTwo
    {
        [Inject]
        private IAppStateService appStateService { get; set; }

        [CascadingParameter]
        private CascadingAppState cascadingAppState { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((IAuthenticationService)authenticationService).GuardRoute();
            appStateService.OnChange += StateHasChanged;
        }

        private void setTime()
        {
            appStateService.Time = DateTime.Now;
        }
    }
}
