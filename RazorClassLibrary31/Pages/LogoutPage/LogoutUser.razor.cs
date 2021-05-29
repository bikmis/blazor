using Microsoft.AspNetCore.Components;
using RazorClassLibrary31.Services.AuthenticationService;
using RazorClassLibrary31.Services.UserService;

namespace RazorClassLibrary31.Pages.LogoutPage
{
    partial class LogoutUser
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IAuthenticationService authenticationService { get; set; }

        [Inject]
        private IUserService userService { get; set; }

        private void logout() {
            authenticationService.LogoutUser();
        }

    }
}
