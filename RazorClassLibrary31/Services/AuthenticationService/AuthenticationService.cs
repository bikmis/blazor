using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using RazorClassLibrary31.Helper;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.HttpService;
using RazorClassLibrary31.Services.SerializerService;
using RazorClassLibrary31.Services.TokenService;
using RazorClassLibrary31.Services.UserService;
using System.Net.Http;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.AuthenticationService
{
    //AuthenticationService has been injected for IAuthenticationService using AddHttpClient, which is "scoped" and so any property exposed
    //by AuthenticationService does not hold value like singleton. That's why IsLoggedIn is exposed from UserService which is a singleton service.
    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService httpService;
        private HttpClient httpClient;
        private ISerializerService serializerService;
        private IJSRuntime jsRuntime { get; set; }       
        private IUserService userService { get; set; }
        private ITokenService tokenService { get; set; }
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        public AuthenticationService(IHttpService _httpService, HttpClient _httpClient, ISerializerService _serializerService, 
            IJSRuntime _jsRuntime, IUserService _userService, ITokenService _tokenService, AuthenticationStateProvider _authenticationStateProvider)
        {
            httpService = _httpService;
            httpClient = _httpClient;
            serializerService = _serializerService;
            jsRuntime = _jsRuntime;
            userService = _userService;
            tokenService = _tokenService;
            authenticationStateProvider = _authenticationStateProvider;
        }

        public async Task<bool> LoginUser(Login login)
        {
            var response = await httpService.SendAsync(httpClient, HttpMethod.Post, "api/login", login);
            if (response.IsSuccessStatusCode) {
                var token = await serializerService.DeserializeToType<Token>(response);
                //AccessToken in a service property and RefreshToken is saved in session storage of the browser
                tokenService.AccessToken = token.AccessToken;
                await jsRuntime.InvokeVoidAsync("setToSessionStorage", "refresh_token", token.RefreshToken);
                
                //user is created to hydrate user service property
                var user = new User()
                {
                    ID = int.Parse(Utility.ReadToken(token.AccessToken, "id")),
                    Name = Utility.ReadToken(token.AccessToken, "name"),
                    Username = Utility.ReadToken(token.AccessToken, "username"),
                    Email = Utility.ReadToken(token.AccessToken, "email"),
                    IsLoggedIn = true
                };
                userService.User = user;

                ((AuthenticationStateProviderService)authenticationStateProvider).LogIntoUserInterface();
               
                return true;
            }
            return false;
        }

        public void LogoutUser()
        {
            jsRuntime.InvokeVoidAsync("clearSessionStorage");
            userService.User.IsLoggedIn = false;
            ((AuthenticationStateProviderService)authenticationStateProvider).LogOutOfUserInterface();
        }
    }
}
