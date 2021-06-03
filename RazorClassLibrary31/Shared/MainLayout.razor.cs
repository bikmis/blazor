using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.User_Service;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Shared
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
