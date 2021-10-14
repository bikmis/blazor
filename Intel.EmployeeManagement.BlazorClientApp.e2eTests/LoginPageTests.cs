using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.IO;
using System.Threading;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClientApp.e2eTests
{
    public class LoginPageTests
    {       
        private IWebDriver createDriver(string driverName) {
            var driverPath = Path.GetFullPath(@"Drivers");
            IWebDriver driver = null;

            if (driverName == "chrome") {
                var chromeOptions = new ChromeOptions();
                chromeOptions.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
                driver = new ChromeDriver(driverPath, chromeOptions);
            }

            if (driverName == "edge")
            {
                var edgeOptions = new EdgeOptions();
                edgeOptions.BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                driver = new EdgeDriver(driverPath, edgeOptions);
            }

            return driver;
        }

        [Fact(DisplayName = "Test login page validation with Chrome")]
        public void TestLoginPageValidationWithChrome()
        {
            //Arrange
            var driver = createDriver("chrome");
            //Act and Assert
            loginActAndAssert(driver);
        }

        [Fact(DisplayName = "Test login page validation with Edge")]
        public void TestLoginPageValidationWithEdge()
        {
            //Arrange
            var driver = createDriver("edge");
            //Act and Assert
            loginActAndAssert(driver);
        }

        private void loginActAndAssert(IWebDriver driver) {
            //Act
            driver.Navigate().GoToUrl("https://localhost:8080");
            Thread.Sleep(3000); //wait for 3 seconds 
            var logingBtn = driver.FindElement(By.Id("login"));
            logingBtn.Click();
            var usernameValidationMessage = driver.FindElement(By.Id("username-validation-message")).Text;
            var passwordValidationMessage = driver.FindElement(By.Id("password-validation-message")).Text;

            //Assert
            Assert.True(usernameValidationMessage == "The Username field is required.");
            Assert.True(passwordValidationMessage == "The Password field is required.");
        }
    }
}
