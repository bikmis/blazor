using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.LoginService;

namespace RazorClassLibrary31.Pages.LoginPage
{
    public partial class LoginUser
    {
        [Inject]
        private ILoginService loginService { get; set; }

        private Login login { get; set; } = new Login();

        [Inject]
        private NavigationManager navigationManager { get; set; }

        private async Task loginUser() {
            var isLoggedIn = await loginService.LoginUser(login);
            if (isLoggedIn) {
                navigationManager.NavigateTo("/");
            }
        }
    }
}
