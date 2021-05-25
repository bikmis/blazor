using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.LoginService
{
    public class AuthenticationService : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim("name", "Bikash", ClaimValueTypes.String),
                    new Claim("email", "bikashmishra.developer@gmail.com", ClaimValueTypes.String)
                }, "Fake authentication type");

            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
