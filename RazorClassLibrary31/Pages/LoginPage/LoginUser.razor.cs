using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.Authentication_Service;
using RazorClassLibrary31.Services.User_Service;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.LoginPage
{
    public partial class LoginUser
    {
        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        private Login login { get; set; } = new Login();

        [Parameter]
        public EventCallback<User> OnLoggedIn { get; set; }

        [Inject]
        private IUserService userService { get; set; }

        private async Task loginUser()
        {
            await ((AuthenticationService)authenticationService).LoginUser(login);
            await OnLoggedIn.InvokeAsync(userService.User);
        }
    }
}
