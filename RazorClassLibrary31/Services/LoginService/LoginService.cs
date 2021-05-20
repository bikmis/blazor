using RazorClassLibrary31.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.LoginService
{
    public class LoginService : ILoginService
    {
        HttpClient httpClient;

        public LoginService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<Token> LoginUser(Login login) { 
            var loginJson = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/login", loginJson);
            var jwt = response.Content.ReadAsStringAsync().Result;
            var token = JsonSerializer.Deserialize<Token>(jwt, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return token;
        }

    }
}
