using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.HomePage
{
    public partial class Index
    {
        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override void OnInitialized()
        {
            await ((IAuthenticationService)authenticationService).GuardRoute();
            await jsRuntime.InvokeVoidAsync("writeToConsole", "You have landed on the home page.");
            base.OnInitialized();
        }
    }
}
