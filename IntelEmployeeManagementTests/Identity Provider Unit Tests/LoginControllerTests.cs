using Intel.EmployeeManagement.IdentityProvider.Controllers;
using Intel.EmployeeManagement.IdentityProvider.Models.Login;
using Intel.EmployeeManagement.Tests.Identity_Provider_Unit_Tests.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using Xunit;

namespace Intel.EmployeeManagement.Tests.Identity_Provider_Unit_Tests
{
    public class LoginControllerTests
    {
        [Fact(DisplayName = "Test if you receive tokens when you login")]
        public void TestLogin() {
            //Arrange
            var employeeDbContext = LoginService.ProvideEmployeeDbContextWithInMemoryDatabase();
            var configuration = LoginService.ProvideConfiguration();
            var loginController = new LoginController(employeeDbContext, configuration);
            var loginRequest = new LoginRequest() { Username = "bikash@gmail.com", Password = "password" };

            //Act
            var response = loginController.Login(loginRequest);

            //Assert
            var loginResponse = (LoginResponse)((response as OkObjectResult).Value);
            Assert.NotNull(loginResponse.AccessToken);
            Assert.NotNull(loginResponse.RefreshToken);
        }       
    }
}
