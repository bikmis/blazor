using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Services.Authentication_Service;
using RazorClassLibrary31.Services.User_Service;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Shared
{
    public partial class MainLayout
    {
        private string emailAddress { get; set; }

        [Inject]
        private IUserService userService { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        protected override Task OnInitializedAsync()
        {
            emailAddress = userService.User.Email;
            return base.OnInitializedAsync();
        }

        private void logout() {
            ((AuthenticationService)authenticationService).LogoutUser();
            navigationManager.NavigateTo("/");
        }
    }
}
