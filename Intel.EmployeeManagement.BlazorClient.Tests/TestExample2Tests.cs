using Bunit;
using Intel.EmployeeManagement.BlazorClient.Tests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.TestExamplePage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests
{
    //https://docs.microsoft.com/en-us/aspnet/core/blazor/test?view=aspnetcore-5.0

    public class TestExample2Tests : TestContext
    {
        //Unreliable unit test of "#countOfPhotos" element in testexample2 component (depending on api, database and configuration)

        //This test will fail if:
        //1. The component under test (#getPhotos element)'s text changes, then expected value needs to be changed in the test.
        //2. Number of photos may change in the database and the test will fail, then you need to modify this test for it to pass.
        //3. Database server is down or table is modified. The test will never pass until the database is back on.
        //4. API server is down. The test will never pass untill the API server is up and running.
        //5. The base address of the API is changed in the configuration file. Then you need to modify this test for it to pass.

        private IRenderedComponent<TestExample2> createTestExample2Component() {
            var configurationService = new ServiceDescriptor(typeof(IConfiguration), new MockConfigurationService());
            var httpClientService = new ServiceDescriptor(typeof(HttpClient), new HttpClient());
            Services.Add(configurationService);
            Services.Add(httpClientService);
            var cut = RenderComponent<TestExample2>();
            return cut;
        }

        [Fact]
        public void Number_of_photos_is_5000_when_you_click_on_get_photos_button()
        {
            //Arrange
            var cut = createTestExample2Component();

            //Act
            cut.Find("#getPhotos").Click();
            Thread.Sleep(5000); //After we click the button, we need to wait for a while to get photos over an http call
            var text = cut.Find("#countOfPhoto").TextContent;

            //Assert
            text.MarkupMatches("Count of photos: 5000");
        }

        [Fact]
        public void Count_of_photos_does_not_exist_when_you_click_clear_button_after_you_click_get_photos_button()
        {
            //Arrange
            var cut = createTestExample2Component();

            //Act
            cut.Find("#getPhotos").Click();
            Thread.Sleep(5000); //After we click the button, we need to wait for a while to get photos over an http call
            var markup = cut.Markup;
            var countOfPhotosExists = markup.Contains("Count of photos");

            //Assert
            Assert.True(countOfPhotosExists == true);

            //Act
            cut.Find("#clearPhotos").Click();
            markup = cut.Markup;
            countOfPhotosExists = markup.Contains("Count of photos");

            //Assert
            Assert.True(countOfPhotosExists == false);
        }
    }
}
