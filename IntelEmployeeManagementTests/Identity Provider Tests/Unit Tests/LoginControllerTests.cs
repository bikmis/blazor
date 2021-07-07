using Intel.EmployeeManagement.IdentityProvider.Controllers;
using Intel.EmployeeManagement.IdentityProvider.Models.AccessToken;
using Intel.EmployeeManagement.IdentityProvider.Models.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Intel.EmployeeManagement.Tests.Identity_Provider_Tests.Unit_Tests
{
    public class LoginControllerTests
    {
        [Fact(DisplayName = "Test if you receive access and refresh tokens when you login with username and password")]
        public void TestLogin() {
            //Arrange
            var employeeDbContext = SharedService.ProvideEmployeeDbContextWithInMemoryDatabase();
            var configuration = SharedService.ProvideConfiguration();
            var loginController = new LoginController(employeeDbContext, configuration);
            var loginRequest = new LoginRequest() { Username = "bikash@gmail.com", Password = "password" };

            //Act
            var response = loginController.Login(loginRequest);

            //Assert
            var loginResponse = (LoginResponse)(response as OkObjectResult).Value;
            Assert.NotNull(loginResponse.AccessToken);
            Assert.NotNull(loginResponse.RefreshToken);
        }

        [Fact(DisplayName = "Test if you receive access token via a request with refresh token")]
        public void TestAccessToken() {
            //Arrange
            var employeeDbContext = SharedService.ProvideEmployeeDbContextWithInMemoryDatabase();
            var configuration = SharedService.ProvideConfiguration();
            var loginController = new LoginController(employeeDbContext, configuration);
            var loginRequest = new LoginRequest() { Username = "bikash@gmail.com", Password = "password" };
            var loginResponse = loginController.Login(loginRequest);
            var loginResponseObj = (LoginResponse)((loginResponse as OkObjectResult).Value);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = $"Bearer {loginResponseObj.RefreshToken}";
            loginController.ControllerContext.HttpContext = httpContext;

            //Act
            var accessTokenResponse = loginController.CreateAccessToken();
            var accessTokenResponseObj = (AccessTokenResponse)(accessTokenResponse as OkObjectResult).Value;

            //Assert
            Assert.NotNull(accessTokenResponseObj.AccessToken);
        }
    }
}
