using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.LoginService;
using RazorClassLibrary31.Services.RouteGuardService;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.LoginPage
{
    public partial class LoginUser
    {
        [Inject]
        private ILoginService loginService { get; set; }

        private Login login { get; set; } = new Login();

        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        private async Task loginUser()
        {
            var isLoggedIn = await loginService.LoginUser(login);
            if (isLoggedIn)
            {
                ((RouteGuardService)authenticationStateProvider).LogIntoUserInterface();
            }
        }
    }
}
