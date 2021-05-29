using Microsoft.AspNetCore.Components;
using RazorClassLibrary31.Services.LoginService;
using RazorClassLibrary31.Services.UserService;

namespace RazorClassLibrary31.Pages.LogoutPage
{
    partial class LogoutUser
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private ILoginService loginService { get; set; }

        [Inject]
        private IUserService userService { get; set; }

        private void logout() {
            loginService.LogoutUser(userService.User);
            navigationManager.NavigateTo("/");
        }

    }
}
