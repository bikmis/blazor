using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Intel.EmployeeManagement.WebAPI.IntegrationTestsQaEnv.Helper
{
    public class Token
    {
        private static string identityProviderTestSiteBaseUrl = Helper.Configuration.Build().GetSection("IdentityProviderTestSiteBaseUrl").Value;
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public static Token Get()
        {
            var client = new HttpClient() { BaseAddress = new Uri(identityProviderTestSiteBaseUrl) };
            var data = new { username = "bikash@gmail.com", password = "password" };
            var httpContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = client.PostAsync("api/login", httpContent).Result.Content.ReadAsStringAsync().Result;
            var token = JsonSerializer.Deserialize<Token>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return token;
        }
    }
}
