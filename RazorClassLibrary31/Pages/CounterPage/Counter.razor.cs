using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Services.Authentication_Service;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.CounterPage
{
    public partial class Counter
    {
        private int currentCount = 0;

        private void incrementCount()
        {
            currentCount++;
        }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
        }
    }
}
