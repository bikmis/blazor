using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Shared
{
    public partial class EmployeeLayout
    {
        private string emailAddress { get; set; }

        [Inject]
        private IAppStateService appStateService { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        protected override Task OnInitializedAsync()
        {
            emailAddress = appStateService.User.Email;
            return base.OnInitializedAsync();
        }

        private void logout() {
            ((AuthenticationService)authenticationService).LogoutUser();
            navigationManager.NavigateTo("/");
        }
    }
}
