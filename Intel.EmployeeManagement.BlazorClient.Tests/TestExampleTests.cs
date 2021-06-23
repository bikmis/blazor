using Bunit;
using Intel.EmployeeManagement.BlazorClient.Tests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.TestExamplePage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var configurationService = new ServiceDescriptor(typeof(IConfiguration), new FakeConfigurationService());
            Services.Add(configurationService);

            var cut = RenderComponent<TestExample>();

            Thread.Sleep(5000);

            cut.Find("div").MarkupMatches("<div>Count of photos: 5000</div>");
        }
    }
}
