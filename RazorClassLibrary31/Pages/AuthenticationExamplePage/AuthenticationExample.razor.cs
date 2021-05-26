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

        protected async override Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var isAuthenticated = authenticationState.User.Identity.IsAuthenticated;  //true
            var name = authenticationState.User.Identity.Name;
            var authenticationType = authenticationState.User.Identity.AuthenticationType; //Fake authentication type

            claims = authenticationState.User.Claims.ToList();
            var nameFromClaim = authenticationState.User.Claims.ToList().Where(claim => claim.Type == ClaimTypes.Name).FirstOrDefault().Value;
            var emailFromClaim = authenticationState.User.Claims.ToList().Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault().Value;            
        }
    }
}
