using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Services.RouteGuardService;

namespace RazorClassLibrary31.Pages.LogoutPage
{
    partial class LogoutUser
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        private void logout() {
            ((RouteGuardService)authenticationStateProvider).LogOutOfUserInterface();
            navigationManager.NavigateTo("/");
        }

    }
}
