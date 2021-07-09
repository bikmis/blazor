using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace Intel.EmployeeManagement.BlazorClient.UnitTests.Services
{
    public class MockConfigurationService : IConfiguration
    {
        private string photoServiceBaseAddress = "https://jsonplaceholder.typicode.com/";

        public string this[string key] { get => photoServiceBaseAddress; set => photoServiceBaseAddress = value; }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new System.NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}
