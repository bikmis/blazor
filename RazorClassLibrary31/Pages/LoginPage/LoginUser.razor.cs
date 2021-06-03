using Intel.Personnel.RazorClassLibrary.Models;
using Intel.Personnel.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace Intel.Personnel.RazorClassLibrary.Pages.LoginPage
{
    public partial class LoginUser
    {
        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        private Login login { get; set; } = new Login();

        private async Task loginUser()
        {
            await ((AuthenticationService)authenticationService).LoginUser(login);
        }
    }
}
