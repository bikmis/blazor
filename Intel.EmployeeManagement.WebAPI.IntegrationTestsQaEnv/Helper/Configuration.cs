using Microsoft.Extensions.Configuration;
using System.IO;

namespace Intel.EmployeeManagement.WebAPI.IntegrationTestsQaEnv.Helper
{
    public class Configuration
    {
        public static IConfiguration Build()
        {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
