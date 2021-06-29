using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests.e2e_Tests
{
    public class TestBlazorClientApp
    {       
        [Fact]
        public void TestAppInChrome()
        {
            var options = new ChromeOptions();
            options.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            IWebDriver driver = new ChromeDriver(@"C:\Users\Bikash\source\repos\blazor\Intel.EmployeeManagement.BlazorClient.Tests\e2e Tests\ChromeDriver", options);
            driver.Navigate().GoToUrl("http://localhost:8075");            
        }
    }
}
