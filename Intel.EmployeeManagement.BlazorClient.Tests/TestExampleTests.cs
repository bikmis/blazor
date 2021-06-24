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
        //Reliable unit test of "#countOfPhotos" element in testexample1 component (without depending on api and database)
        [Fact]
        public void Number_of_photos_is_3_when_you_click_on_get_photos_button_in_testexample1_component()
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

        //Unreliable unit test of "#countOfPhotos" element in testexample2 component (depending on api and database)
        [Fact]
        public void Number_of_photos_is_5000_when_you_click_on_get_photos_button_in_testexample2_component()
        {
            //Arrange
            var configurationService = new ServiceDescriptor(typeof(IConfiguration), new MockConfigurationService());
            var httpClientService = new ServiceDescriptor(typeof(HttpClient), new HttpClient());
            Services.Add(configurationService);
            Services.Add(httpClientService);
            var cut = RenderComponent<TestExample2>();

            //Act
            cut.Find("#getPhotos").Click();
            Thread.Sleep(5000); //After we click the button, we need to wait for a while to get photos over an http call
            var text = cut.Find("#countOfPhoto").TextContent;

            //Assert
            text.MarkupMatches("Count of photos: 5000");
        }
    }
}
