using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Intel.Personnel.RazorClassLibrary.Services.Authentication_Service;
using System.Threading.Tasks;

namespace Intel.Personnel.RazorClassLibrary.Pages.AuthenticationExamplePage
{
    partial class AuthenticationExample
    {   

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }
      

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
        }

    }
}
