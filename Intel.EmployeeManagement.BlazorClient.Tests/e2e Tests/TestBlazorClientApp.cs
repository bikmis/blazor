using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests.e2e_Tests
{
    public class TestBlazorClientApp
    {       
        [Fact]
        public void TestAppInChrome()
        {
            //Arrange
            var options = new ChromeOptions();
            options.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            IWebDriver driver = new ChromeDriver(@"C:\Users\Bikash\source\repos\blazor\Intel.EmployeeManagement.BlazorClient.Tests\e2e Tests\ChromeDriver", options);
            
            //Act
            driver.Navigate().GoToUrl("http://localhost:8075");
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
