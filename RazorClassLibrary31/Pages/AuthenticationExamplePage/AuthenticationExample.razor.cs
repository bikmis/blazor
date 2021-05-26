using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.AuthenticationExamplePage
{
    partial class AuthenticationExample
    {        
        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }
      
        private IEnumerable<Claim> claims { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private async Task DoSomethingIfAuthenticated() {
            var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var isAuthenticated = authenticationState.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                //navigate to a page that needs authentication/authorization
                var name = authenticationState.User.Identity.Name;
                var authenticationType = authenticationState.User.Identity.AuthenticationType;

                claims = authenticationState.User.Claims.ToList();
                var nameFromClaim = authenticationState.User.Claims.ToList().Where(claim => claim.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
                var emailFromClaim = authenticationState.User.Claims.ToList().Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            }
        }
    }
}
