using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intel.EmployeeManagement.BlazorClient.Tests.Unit_Tests.Services
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
