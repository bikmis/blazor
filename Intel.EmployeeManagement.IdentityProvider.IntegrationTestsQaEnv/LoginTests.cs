using Intel.EmployeeManagement.IdentityProvider.IntegrationTestsQaEnv.Response;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Intel.EmployeeManagement.IdentityProvider.IntegrationTestsQaEnv
{
    public class LoginTests
    {
        [Fact(DisplayName = "Successful Login")]
        public void LoginWithSuccess()
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:8081/") };
            var data = new { username = "bikash@gmail.com", password = "password"};
            var httpContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = client.PostAsync("api/login", httpContent).Result.Content.ReadAsStringAsync().Result;
            var token = JsonSerializer.Deserialize<Token>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            Assert.NotNull(token.AccessToken);
            Assert.NotNull(token.RefreshToken);
        }

        [Fact(DisplayName = "Failed Login")]
        public void LoginWithFailure()
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:8081/") };
            var data = new { username = "bikash@gmail.com", password = "passwordxxx" };
            var httpContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var responseStatusCode = client.PostAsync("api/login", httpContent).Result.StatusCode;
            Assert.True(responseStatusCode.ToString() == "Unauthorized");
            Assert.True((int)responseStatusCode == 401);
        }

    }
}
