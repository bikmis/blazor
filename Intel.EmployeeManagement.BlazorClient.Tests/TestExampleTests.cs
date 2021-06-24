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

        //This tet will fail:
        //1. The component under test (#getPhotos element)'s text changes, then expected value needs to be changed in the test

        //This test will not fail if:
        //1. Number of photos may change in the database and the test will fail. 
        //2. Database server is down or table is modified.
        //3. API server is down or API is modified.
        //4. The base address of the API is changed.
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

        //Unreliable unit test of "#countOfPhotos" element in testexample2 component (depending on api, database and configuration)

        //This test will fail if:
        //1. The component under test (#getPhotos element)'s text changes, then expected value needs to be changed in the test.
        //2. Number of photos may change in the database and the test will fail, then you need to modify this test for it to pass.
        //3. Database server is down or table is modified. The test will never pass until the database is back on.
        //4. API server is down. The test will never pass untill the API server is up and running.
        //5. The base address of the API is changed in the configuration file. Then you need to modify this test for it to pass.
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
