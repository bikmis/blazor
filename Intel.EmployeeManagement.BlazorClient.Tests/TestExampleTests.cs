using Bunit;
using Intel.EmployeeManagement.BlazorClient.Tests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.TestExamplePage;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Photo_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests
{
    //https://docs.microsoft.com/en-us/aspnet/core/blazor/test?view=aspnetcore-5.0

    public class TestExampleTests : TestContext
    {
        [Fact]
        public void NumberOfPhotosIs3()
        {
            //Arrange
            var photoService = new ServiceDescriptor(typeof(IPhotoService), new MockPhotoService());
            Services.Add(photoService);
            var cut = RenderComponent<TestExample1>();

            //Act
            cut.Find("#getPhotos").Click();
            var text = cut.Find("#countOfPhoto").TextContent;

            //Assert
            text.MarkupMatches("Count of photos: 3");
        }

        [Fact]
        public void NumberOfPhotosIs5000()
        {
            //Arrange
            var configurationService = new ServiceDescriptor(typeof(IConfiguration), new MockConfigurationService());
            var httpClientService = new ServiceDescriptor(typeof(HttpClient), new HttpClient());
            Services.Add(configurationService);
            Services.Add(httpClientService);
            var cut = RenderComponent<TestExample2>();

            //Act
            cut.Find("#getPhotos").Click();
            Thread.Sleep(5000);
            var text = cut.Find("#countOfPhoto").TextContent;

            //Assert
            text.MarkupMatches("Count of photos: 5000");
        }




    }
}
