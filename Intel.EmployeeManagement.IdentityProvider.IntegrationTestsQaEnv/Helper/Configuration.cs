using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Intel.EmployeeManagement.IdentityProvider.IntegrationTestsQaEnv.Helper
{
    public class Configuration
    {
        public static IConfiguration Build()
        {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
