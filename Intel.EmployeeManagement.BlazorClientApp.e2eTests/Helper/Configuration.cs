using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Intel.EmployeeManagement.BlazorClientApp.e2eTests.Helper
{
    public class Configuration
    {
        public static IConfiguration Get() {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
