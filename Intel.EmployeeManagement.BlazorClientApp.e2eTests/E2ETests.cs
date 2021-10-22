using Intel.EmployeeManagement.BlazorClientApp.e2eTests.Helper;
using Intel.EmployeeManagement.BlazorClientApp.e2eTests.Pages;
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
            var testSiteUrl = Configuration.Build().GetSection("BlazorUiTestSiteBaseUrl").Value;

            //Act and Assert
            drivers.ForEach(driver =>
            {
                LoginPage.TestLoginValidation(driver, testSiteUrl);
                Thread.Sleep(3000);
            });           
        }
    }
}
