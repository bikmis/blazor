using Microsoft.JSInterop;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.HttpService;
using RazorClassLibrary31.Services.SerializerService;
using RazorClassLibrary31.Services.TokenService;
using System.Net.Http;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private IHttpService httpService;
        private HttpClient httpClient;
        private ITokenService tokenService;
        private ISerializerService serializerService;
        private IJSRuntime jsRuntime { get; set; }

        public LoginService(IHttpService _httpService, HttpClient _httpClient, ITokenService _tokenService, ISerializerService _serializerService, IJSRuntime _jsRuntime)
        {
            httpService = _httpService;
            httpClient = _httpClient;
            tokenService = _tokenService;
            serializerService = _serializerService;
            jsRuntime = _jsRuntime;
        }

        public async Task<bool> LoginUser(Login login)
        {
            var response = await httpService.SendAsync(httpClient, HttpMethod.Post, "api/login", login);
            if (response.IsSuccessStatusCode) {
                var token = await serializerService.DeserializeToType<Token>(response);
                tokenService.AccessToken = token.AccessToken;
                tokenService.RefreshToken = token.RefreshToken;
                await jsRuntime.InvokeVoidAsync("setToSessionStorage", "refresh_token", token.RefreshToken);
                return true;
            }
            return false;
        }

    }
}
