using Bunit;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.TestExamplePage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests
{
    //https://docs.microsoft.com/en-us/aspnet/core/blazor/test?view=aspnetcore-5.0

    public class TestExampleTests : TestContext
    {
        [Fact]
        public void NumberOfPhotosIs5000()
        {
            var configurationService = new ServiceDescriptor(typeof(IConfiguration), new ConfigurationService());
            Services.Add(configurationService);

            var cut = RenderComponent<TestExample>();

            Thread.Sleep(5000);

            cut.Find("div").MarkupMatches("<div>Count of photos: 5000</div>");
        }
    }

    public class ConfigurationService : IConfiguration
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
