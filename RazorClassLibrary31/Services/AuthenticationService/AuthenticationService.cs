using Intel.EmployeeManagement.RazorClassLibrary.Shared;
using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service
{
    //Create AuthenticationStateService that implements AuthenticationStateProvider
    //Put the dependency code in Program.Main for client side and in Startup.cs for server side Blazor.
    //Add builder.Services.AddOptions(); and builder.Services.AddAuthorizationCore(); in the Program.Main for client side Blazor, but not required for the server side.
    //You can now use <CascadingAuthenticationState></CascadingAuthenticationState> etc and @context etc in a component
    //or [Inject] private AuthenticationStateProvider authenticationStateProvider { get; set; } in the code-behind file of a component


    //https://docs.microsoft.com/en-us/dotnet/api/system.security.principal.iidentity.authenticationtype?view=net-5.0
    //Basic authentication, NTLM, Kerberos, and Passport are examples of authentication types.

    public class AuthenticationService : AuthenticationStateProvider
    {
        private IAppStateService appStateService;
        private IJSRuntime jsRuntime;
        private HttpClient httpClient;

        public AuthenticationService(IAppStateService _appStateService, IJSRuntime _jsRuntime, HttpClient _httpClient)
        {
            appStateService = _appStateService;
            jsRuntime = _jsRuntime;
            httpClient = _httpClient;
        }

        //When a page is refreshed or the application loads for the first time, the following method (GetAuthenticationStateAsync) runs.
        //Authorization requires a cascading parameter of type Task<AuthenticationState>. Use CascadingAuthenticationState to supply this.
        //The first time the application loads, this runs and returns createLoggedOutState(), with that Authorization fails and Login screen <LoginUser /> appears from 
        //<NotAuthorized> section of MasterLayout.razor. Once you enter your username and password and click Login, AuthenticationService.LoginUser runs and among other things
        //which will run LoginUser that will notify with createLoggedInState, which then lets the user in past the <Authorized> section of MasterLayout.razor
        //Every time you navigate to a page with [Authorize] attribute, Authorization runs and is successful or fails based on createLoggedOutState() or createLoggedInState().
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var refreshToken = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "refresh_token");

            //For Blazor Server side, appService.User.IsLoggedIn is true and goes to the first "if" condition if a page is refreshed,
            //but for client side, it is false and goes to the next "else if" condition if a page is refreshed.
            //For server side Blazor, service variables don't lose value when the browser is refreshed because of circuit(SignalR connection
            //which can also tolerate temporary network interruptions.)
            if (appStateService.User.IsLoggedIn)
            {
                return await createLoggedInState(appStateService.AccessToken);
            }
            else if (refreshToken != null)
            {
                var response = await GetAccessToken();
                //if response comes back ok with access token, then user stays logged in.
                if (response.IsSuccessStatusCode)
                {
                    var token = await appStateService.Deserialize<Token>(response);
                    appStateService.User = createUserFromToken(token);
                    appStateService.AccessToken = token.AccessToken;
                    return await createLoggedInState(appStateService.AccessToken);
                }
            }

            //if response fails, that means refreshToken has expired or is not available in the sessionStorage, then the user is logged out and back on the login page.
            await jsRuntime.InvokeVoidAsync("clearSessionStorage");
            appStateService.User = new User();
            return await createLoggedOutState();
        }

        public async Task<HttpResponseMessage> LoginUser(Login login)
        {
            var response = await loginUser(login);
            if (response.IsSuccessStatusCode)
            {
                var token = await appStateService.Deserialize<Token>(response);
                //AccessToken in a appService property and RefreshToken is saved in session storage of the browser
                appStateService.AccessToken = token.AccessToken;
                await jsRuntime.InvokeVoidAsync("setToSessionStorage", "refresh_token", token.RefreshToken);

                //user is created to hydrate appService property
                appStateService.User = createUserFromToken(token);

                NotifyAuthenticationStateChanged(createLoggedInState(token.AccessToken));
            }

            return response;
        }

        public void LogoutUser()
        {
            jsRuntime.InvokeVoidAsync("clearSessionStorage");
            appStateService.User = new User();
            NotifyAuthenticationStateChanged(createLoggedOutState());
        }

        public async Task<HttpResponseMessage> GetAccessToken()
        {
            return await sendAsync("api/accessToken", null);
        }

        private async Task<HttpResponseMessage> loginUser(object data)
        {
            return await sendAsync("api/login", data);
        }

        private async Task<HttpResponseMessage> sendAsync(string url, object data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var refreshToken = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "refresh_token");
            if (refreshToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);
            }
            request.Content = appStateService.Serialize(data); //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            var response = await httpClient.SendAsync(request);
            return response;
        }

        public async Task GuardRoute()
        {
            var refreshToken = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "refresh_token");

            if (Utility.IsTokenExpired(appStateService.AccessToken))
            {
                if (refreshToken == null || Utility.IsTokenExpired(refreshToken))
                {
                    LogoutUser();
                    return;
                }
                //if access token has expired, but not refresh token, then do the following, then get a new access token.
                var response = await GetAccessToken();
                if (response.IsSuccessStatusCode)
                {
                    var token = await appStateService.Deserialize<Token>(response);
                    appStateService.AccessToken = token.AccessToken;
                }
                else {
                    LogoutUser();
                    return;
                }               
            }
        }

        private User createUserFromToken(Token token)
        {
            var user = new User()
            {
                ID = int.Parse(Utility.ReadToken(token.AccessToken, "id")),
                Name = Utility.ReadToken(token.AccessToken, "name"),
                Username = Utility.ReadToken(token.AccessToken, "username"),
                Email = Utility.ReadToken(token.AccessToken, "email"),
                IsLoggedIn = true,
                Roles = Utility.GetRoles(token.AccessToken).ToList()
            };
            return user;
        }

        private async Task<AuthenticationState> createLoggedInState(string token)
        {
            var claims = Utility.GetRoles(token).Select(role => new Claim(ClaimTypes.Role, role)).ToList();            
            var emailClaim = new Claim(ClaimTypes.Email, Utility.ReadToken(token, "email"));
            var nameClaim = new Claim(ClaimTypes.Name, Utility.ReadToken(token, "username"));
            claims.Add(emailClaim);
            claims.Add(nameClaim);
            var identity = new ClaimsIdentity(claims, "UiAuthenticationType");
            var user = new ClaimsPrincipal(identity);
            var loggedInState = Task.FromResult(new AuthenticationState(user));
            return await loggedInState;
        }

        private async Task<AuthenticationState> createLoggedOutState()
        {
            var identityNotAuthorized = new ClaimsIdentity(); // Not authorized, to be authorized ClaimsIdentity needs to have claims and/or authenticationType
            var principalLoggedIn = new ClaimsPrincipal(identityNotAuthorized);
            var loggedOutState = await Task.FromResult(new AuthenticationState(principalLoggedIn));
            return loggedOutState;
        }

    }
}
