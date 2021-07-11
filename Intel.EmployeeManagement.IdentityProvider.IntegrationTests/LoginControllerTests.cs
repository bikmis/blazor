using Intel.EmployeeManagement.Data.Entities;
using Intel.EmployeeManagement.IdentityProvider.Models.AccessToken;
using Intel.EmployeeManagement.IdentityProvider.Models.Login;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Intel.EmployeeManagement.IdentityProvider.IntegrationTests
{
    //https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0
    //At the above site, check "Test app prerequisites" which states that the project should have "<Project Sdk="Microsoft.NET.Sdk.Web">" instead of "<Project Sdk="Microsoft.NET.Sdk">" i.e. specify the Web SDK
    //and reference the Microsoft.AspNetCore.Mvc.Testing package.
    public class LoginControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public LoginControllerTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact(DisplayName = "Get tokens via login with username and password")]
        public async Task Test_Login()
        {
            //Arrange
            var client = factory.CreateClient();
            var request = new LoginRequest() { Username = "bikash@gmail.com", Password = "password" };
            var httpContent = SharedService.Serialize(request);

            //Act
            var response = await client.PostAsync($"api/login", httpContent);
            var loginResponse = SharedService.DeserializeToType<LoginResponse>(response);

            //Assert
            Assert.NotNull(loginResponse.AccessToken);
            Assert.NotNull(loginResponse.RefreshToken);
        }

        [Fact(DisplayName = "Get access token with request using refresh token")]
        public async Task Test_AccessToken() {
            //Arrange
            var client = factory.CreateClient();
            var configuration = SharedService.ProvideConfiguration();
            var refreshToken = SharedService.CreateJwt(new User() { ID = 1, Username = "Bikash", Password = "password", Email = "bikash@gmail.com" }, new List<string>() { "admin" }, configuration["RefreshTokenSecurityKey"], configuration["TokenIssuer"], configuration["RefreshTokenAudience"], 100);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken);

            //Act
            var response = await client.PostAsync($"api/accessToken", null);
            var accessTokenResponse = SharedService.DeserializeToType<AccessTokenResponse>(response);

            //Assert
            Assert.NotNull(accessTokenResponse.AccessToken);
        }
    }
}
