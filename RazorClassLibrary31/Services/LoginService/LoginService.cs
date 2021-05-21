using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.TokenService;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private HttpClient httpClient;
        private ITokenService tokenService;

        public LoginService(HttpClient _httpClient, ITokenService _tokenService)
        {
            httpClient = _httpClient;
            tokenService = _tokenService;
        }

        public async Task<bool> LoginUser(Login login)
        {
            var loginJson = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/login", loginJson);
            var jwt = response.Content.ReadAsStringAsync().Result;
            var token = JsonSerializer.Deserialize<Token>(jwt, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            tokenService.Jwt = token.Jwt;
            return true;
        }

    }

}
