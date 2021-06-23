using Bunit;
using Intel.EmployeeManagement.BlazorClient.Tests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.TestExamplePage;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Photo_Service;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests
{
    //https://docs.microsoft.com/en-us/aspnet/core/blazor/test?view=aspnetcore-5.0

    public class TestExampleTests : TestContext
    {
        [Fact]
        public void NumberOfPhotosIs3()
        {
            // var configurationService = new ServiceDescriptor(typeof(IConfiguration), new MockConfigurationService());
            // var httpClientService = new ServiceDescriptor(typeof(HttpClient), new MockHttpClient().HttpClient);
            // Services.Add(configurationService);
            // Services.Add(httpClientService);

            var photoService = new ServiceDescriptor(typeof(IPhotoService), new MockPhotoService());
            Services.Add(photoService);

            var cut = RenderComponent<TestExample>();

           // Thread.Sleep(5000);

            cut.Find("#countOfPhoto").MarkupMatches("<div id=countOfPhoto>Count of photos: 3</div>");
        }
    }
}
