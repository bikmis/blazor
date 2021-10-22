using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;
using System.IO;

namespace Intel.EmployeeManagement.BlazorClientApp.e2eTests.Helper
{
    public class WebDriver
    {
        public static List<IWebDriver> Create()
        {
            var driverPath = Path.GetFullPath(@"Helper\Drivers");
            List<IWebDriver> drivers = new List<IWebDriver>();

            var chromeOptions = new ChromeOptions();
            chromeOptions.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            drivers.Add(new ChromeDriver(driverPath, chromeOptions));

          /*  var edgeOptions = new EdgeOptions();
            edgeOptions.BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
            drivers.Add(new EdgeDriver(driverPath, edgeOptions));
          */
            return drivers;
        }
    }
}
