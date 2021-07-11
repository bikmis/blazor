﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClientApp.e2eTests
{
    public class LoginPageTests
    {       
        private IWebDriver createChromeDriver() {
            var options = new ChromeOptions();
            options.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            var chromeDriverPath = Path.GetFullPath(@"Drivers");
            IWebDriver driver = new ChromeDriver(chromeDriverPath, options);
            return driver;
        }

        [Fact(DisplayName = "Test login page validation")]
        public void TestLoginPageValidation()
        {
            //Arrange
            var driver = createChromeDriver();

            //Act
            driver.Navigate().GoToUrl("http://localhost:8090");
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