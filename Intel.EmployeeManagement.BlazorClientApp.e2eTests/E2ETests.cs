using Intel.EmployeeManagement.BlazorClientApp.e2eTests.Helper;
using Intel.EmployeeManagement.BlazorClientApp.e2eTests.Pages;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using Xunit;


namespace Intel.EmployeeManagement.BlazorClientApp.e2eTests
{
    public class E2ETests
    {
        [Fact(DisplayName = "Test login validation")]
        public void TestLoginValidation()
        {
            //Arrange
            var drivers = WebDriver.Create();
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var testSiteUrl = configuration.GetSection("TestSiteUrl").Value;

            //Act and Assert
            drivers.ForEach(driver =>
            {
                LoginPage.TestValidation(driver, testSiteUrl);
                driver.Close();
            });           
        }
    }
}
