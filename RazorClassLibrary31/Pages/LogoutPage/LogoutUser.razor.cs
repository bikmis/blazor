using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Services.Authentication_Service;
using RazorClassLibrary31.Services.User_Service;

namespace RazorClassLibrary31.Pages.LogoutPage
{
    partial class LogoutUser
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        [Inject]
        private IUserService userService { get; set; }

        private void logout() {
            ((AuthenticationService)authenticationService).LogoutUser();
            //Change the url "logout" back to home "/"
            navigationManager.NavigateTo("/");
        }

    }
}
