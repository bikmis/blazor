using Intel.EmployeeManagement.BlazorClientApp.e2eTests.Helper;
using Intel.EmployeeManagement.BlazorClientApp.e2eTests.Pages;
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
            var testSiteUrl = Configuration.Get().GetSection("TestSiteUrl").Value;

            //Act and Assert
            drivers.ForEach(driver =>
            {
                LoginPage.TestValidation(driver, testSiteUrl);
            });           
        }
    }
}
