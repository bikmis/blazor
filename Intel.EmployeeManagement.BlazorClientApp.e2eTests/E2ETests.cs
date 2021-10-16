using Intel.EmployeeManagement.BlazorClientApp.e2eTests.Helper;
using Intel.EmployeeManagement.BlazorClientApp.e2eTests.Pages;
using Xunit;


namespace Intel.EmployeeManagement.BlazorClientApp.e2eTests
{
    public class E2ETests
    {
        string url = "https://localhost:8080";

        [Fact(DisplayName = "Test login validation")]
        public void TestLoginValidation()
        {
            //Arrange
            var drivers = WebDriver.Create();

            drivers.ForEach(driver =>
            {
                //Act and Assert
                LoginPage.TestValidation(driver, url);
            });
        }

        
    }
}
