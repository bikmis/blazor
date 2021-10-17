using OpenQA.Selenium;
using System.Threading;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClientApp.e2eTests.Pages
{
    public class LoginPage
    {
        public static void TestLoginValidation(IWebDriver driver, string url)
        {
            //Act
            driver.Navigate().GoToUrl(url);
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
