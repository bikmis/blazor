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

        private async Task loginUser()
        {
            await loginService.LoginUser(login);
        }
    }
}
