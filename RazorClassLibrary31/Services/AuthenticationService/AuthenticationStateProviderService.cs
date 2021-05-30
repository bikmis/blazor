using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using RazorClassLibrary31.Helper;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.HttpService;
using RazorClassLibrary31.Services.SerializerService;
using RazorClassLibrary31.Services.TokenService;
using RazorClassLibrary31.Services.UserService;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
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
        private IJSRuntime jsRuntime;
        private HttpClient httpClient;
        private IHttpService httpService;
        private ISerializerService serializerService;
        

        public AuthenticationStateProviderService(IUserService _userService, ITokenService _tokenService, IJSRuntime _jsRuntime, IHttpService _httpService, ISerializerService _serializerService)
        {
            userService = _userService;
            tokenService = _tokenService;
            jsRuntime = _jsRuntime;
            httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:44382/") };
            httpService = _httpService;
            serializerService = _serializerService;
        }

        //When a page is refreshed or the application loads for the first time, the following method runs.
        //Authorization requires a cascading parameter of type Task<AuthenticationState>.Consider using CascadingAuthenticationState to supply this.
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var refreshToken = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "refresh_token");
            await jsRuntime.InvokeVoidAsync("writeToConsole", httpClient.BaseAddress.AbsoluteUri);
            await jsRuntime.InvokeVoidAsync("writeToConsole", refreshToken);

            if (userService.User.IsLoggedIn)
            {
                return await createLoggedInState(tokenService.AccessToken);
            }
            else if (refreshToken != null)
            {
                var response = await httpService.SendAsync(httpClient, HttpMethod.Post, "api/refreshToken", null, refreshToken);
                if (response.IsSuccessStatusCode) {
                    var token = await serializerService.DeserializeToType<Token>(response);
                    tokenService.AccessToken = token.AccessToken;
                    return await createLoggedInState(tokenService.AccessToken);
                }

                await jsRuntime.InvokeVoidAsync("clearSessionStorage");
                userService.User.IsLoggedIn = false;
                return await createLoggedOutState();
            }

            return await createLoggedOutState();
        }

        public void LogIntoUserInterface(string token)
        {
            NotifyAuthenticationStateChanged(createLoggedInState(token));
        }

        public void LogOutOfUserInterface()
        {
            NotifyAuthenticationStateChanged(createLoggedOutState());
        }

        private async Task<AuthenticationState> createLoggedInState(string token)
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, Utility.ReadToken(token, "name"), ClaimValueTypes.String),
                    new Claim(ClaimTypes.Email, Utility.ReadToken(token, "email"), ClaimValueTypes.String)
                }, "Fake authentication type");
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
