using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Services.UserService;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.LoginService
{
    //Create AuthenticationService that implements AuthenticationStateProvider
    //Put the dependency code in Program.Main for client side and in Startup.cs for server side Blazor.
    //Add builder.Services.AddOptions(); and builder.Services.AddAuthorizationCore(); in the Program.Main for client side Blazor, but not required for the server side.
    //You can now use <CascadingAuthenticationState></CascadingAuthenticationState> etc and @context etc in a component
    //or [Inject] private AuthenticationStateProvider authenticationStateProvider { get; set; } in the code-behind file of a component

    //https://docs.microsoft.com/en-us/dotnet/api/system.security.principal.iidentity.authenticationtype?view=net-5.0
    //Basic authentication, NTLM, Kerberos, and Passport are examples of authentication types.

    public class AuthenticationService : AuthenticationStateProvider
    {
        private IUserService userService;

        public AuthenticationService(IUserService _userService)
        {
            userService = _userService;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (userService.User.IsLoggedIn) {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, "Bikash", ClaimValueTypes.String),
                    new Claim(ClaimTypes.Email, "bikashmishra.developer@gmail.com", ClaimValueTypes.String)
                }, "Fake authentication type");

                var user = new ClaimsPrincipal(identity);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
                return Task.FromResult(new AuthenticationState(user));
            }

            var identityNotAuthorized = new ClaimsIdentity(); // Not authorized, to be authorized ClaimsIdentity needs to have claims and/or authenticationType
            var userNotLoggedIn = new ClaimsPrincipal(identityNotAuthorized);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(userNotLoggedIn)));
            return Task.FromResult(new AuthenticationState(userNotLoggedIn));
        }
    }
}
