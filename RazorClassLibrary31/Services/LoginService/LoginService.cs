using Microsoft.JSInterop;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.HttpService;
using RazorClassLibrary31.Services.SerializerService;
using System.Net.Http;
using System.Threading.Tasks;
using RazorClassLibrary31.Services.UserService;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using RazorClassLibrary31.Services.TokenService;

namespace RazorClassLibrary31.Services.LoginService
{
    //LoginService has been injected for ILoginService using AddHttpClient, which is "scoped" and so any property exposed
    //by LoginService does not hold value like singleton. That's why IsLoggedIn is exposed from UserService which is a singleton service.
    public class LoginService : ILoginService
    {
        private IHttpService httpService;
        private HttpClient httpClient;
        private ISerializerService serializerService;
        private IJSRuntime jsRuntime { get; set; }       
        private IUserService userService { get; set; }
        private ITokenService tokenService { get; set; }

        public LoginService(IHttpService _httpService, HttpClient _httpClient, ISerializerService _serializerService, IJSRuntime _jsRuntime, IUserService _userService, ITokenService _tokenService)
        {
            httpService = _httpService;
            httpClient = _httpClient;
            serializerService = _serializerService;
            jsRuntime = _jsRuntime;
            userService = _userService;
            tokenService = _tokenService;
        }

        public async Task<bool> LoginUser(Login login)
        {
            var response = await httpService.SendAsync(httpClient, HttpMethod.Post, "api/login", login);
            if (response.IsSuccessStatusCode) {
                var token = await serializerService.DeserializeToType<Token>(response);
                tokenService.AccessToken = token.AccessToken;
                var id = int.Parse(readToken(token, "id"));
                var name = readToken(token, "name");
                var username = readToken(token, "username");
                var email = readToken(token, "email");
                var user = new User() { ID = id, Name = name, Username = username, Email = email, IsLoggedIn = true };
                userService.User = user;
                await jsRuntime.InvokeVoidAsync("setToSessionStorage", "refresh_token", token.RefreshToken);
                return true;
            }
            return false;
        }

        private string readToken(Token token, string claimType) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token.AccessToken);
            var claimValue = jsonToken.Claims.ToList().Where(claim => claim.Type == claimType).FirstOrDefault()?.Value;
            return claimValue;
        }



    }
}
