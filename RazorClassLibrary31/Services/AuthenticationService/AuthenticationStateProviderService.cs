using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Helper;
using RazorClassLibrary31.Services.TokenService;
using RazorClassLibrary31.Services.UserService;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.AuthenticationService
{
    //Create AuthenticationStateService that implements AuthenticationStateProvider
    //Put the dependency code in Program.Main for client side and in Startup.cs for server side Blazor.
    //Add builder.Services.AddOptions(); and builder.Services.AddAuthorizationCore(); in the Program.Main for client side Blazor, but not required for the server side.
    //You can now use <CascadingAuthenticationState></CascadingAuthenticationState> etc and @context etc in a component
    //or [Inject] private AuthenticationStateProvider authenticationStateProvider { get; set; } in the code-behind file of a component
    

    //https://docs.microsoft.com/en-us/dotnet/api/system.security.principal.iidentity.authenticationtype?view=net-5.0
    //Basic authentication, NTLM, Kerberos, and Passport are examples of authentication types.

    public class AuthenticationStateProviderService : AuthenticationStateProvider
    {
        private IUserService userService;
        private ITokenService tokenService;

        public AuthenticationStateProviderService(IUserService _userService, ITokenService _tokenService)
        {
            userService = _userService;
            tokenService = _tokenService;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (userService.User.IsLoggedIn)
            {
                return createLoggedInState();
            }
            return createLoggedOutState();
        }       

        public void LogIntoUserInterface()
        {
            if (userService.User.IsLoggedIn)
            {
                NotifyAuthenticationStateChanged(createLoggedInState());
            }
        }

        public void LogOutOfUserInterface()
        {
            userService.User.IsLoggedIn = false;
            NotifyAuthenticationStateChanged(createLoggedOutState());
        }

        private Task<AuthenticationState> createLoggedInState()
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, Utility.ReadToken(tokenService.AccessToken, "name"), ClaimValueTypes.String),
                    new Claim(ClaimTypes.Email, Utility.ReadToken(tokenService.AccessToken, "email"), ClaimValueTypes.String)
                }, "Fake authentication type");

            var user = new ClaimsPrincipal(identity);
            var loggedInState = Task.FromResult(new AuthenticationState(user));
            return loggedInState;
        }

        private Task<AuthenticationState> createLoggedOutState()
        {
            var identityNotAuthorized = new ClaimsIdentity(); // Not authorized, to be authorized ClaimsIdentity needs to have claims and/or authenticationType
            var principalLoggedIn = new ClaimsPrincipal(identityNotAuthorized);
            var loggedOutState = Task.FromResult(new AuthenticationState(principalLoggedIn));
            return loggedOutState;
        }
    }
}
