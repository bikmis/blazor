using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using RazorClassLibrary31.Services.Authentication_Service;

namespace RazorClassLibrary31.Pages.HomePage
{
    public partial class Index
    {
        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }


        protected async override void OnInitialized()
        {
            await((AuthenticationService)authenticationService).GuardRoute();
            await jsRuntime.InvokeVoidAsync("writeToConsole", "You have landed on the home page.");
            base.OnInitialized();
        }
    }
}
