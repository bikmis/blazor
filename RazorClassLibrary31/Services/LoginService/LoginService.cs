using Microsoft.JSInterop;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.HttpService;
using RazorClassLibrary31.Services.SerializerService;
using System.Net.Http;
using System.Threading.Tasks;
using RazorClassLibrary31.Services.UserService;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace RazorClassLibrary31.Services.LoginService
{
    //LoginService has been injected for ILoginService using AddHttpClient, which is "scoped" and so any property exposed
    //by LoginService does not hold value like singleton. That's why IsLoggedIn is exposed from TokenService which is a singleton service.
    public class LoginService : ILoginService
    {
        private IHttpService httpService;
        private HttpClient httpClient;
        private ISerializerService serializerService;
        private IJSRuntime jsRuntime { get; set; }       
        private IUserService userService { get; set; }

        public LoginService(IHttpService _httpService, HttpClient _httpClient, ISerializerService _serializerService, IJSRuntime _jsRuntime, IUserService _userService)
        {
            httpService = _httpService;
            httpClient = _httpClient;
            serializerService = _serializerService;
            jsRuntime = _jsRuntime;
            userService = _userService;
        }

        public async Task<bool> LoginUser(Login login)
        {
            var response = await httpService.SendAsync(httpClient, HttpMethod.Post, "api/login", login);
            if (response.IsSuccessStatusCode) {
                var user = await serializerService.DeserializeToType<User>(response);
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadJwtToken(user.AccessToken);
                var email = jsonToken.Claims.ToList().Where(claim => claim.Type == "email").FirstOrDefault().Value;
                user.IsLoggedIn = true;
                user.Email = email;
                userService.User = user;
                await jsRuntime.InvokeVoidAsync("setToSessionStorage", "refresh_token", user.RefreshToken);
                return true;
            }
            return false;
        }

    }
}
