using Intel.EmployeeManagement.IdentityProvider;
using Intel.EmployeeManagement.IdentityProvider.Models.Login;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Intel.EmployeeManagement.IntegrationTests
{
    //https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0
    //At the above site, check "Test app prerequisites" which states that the project should have "<Project Sdk="Microsoft.NET.Sdk.Web">" instead of "<Project Sdk="Microsoft.NET.Sdk">" i.e. specify the Web SDK
    //and reference the Microsoft.AspNetCore.Mvc.Testing package.
    public class IdentityProviderApiIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public IdentityProviderApiIntegrationTests(WebApplicationFactory<Startup> factory)
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
    }
}
