using Bunit;
using Intel.EmployeeManagement.BlazorClient.Tests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.TestExamplePage;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Photo_Service;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests
{
    //https://docs.microsoft.com/en-us/aspnet/core/blazor/test?view=aspnetcore-5.0

    public class TestExample1Tests : TestContext
    {
        //Reliable unit test of "#countOfPhotos" element in testexample1 component (without depending on api and database)

        //This tet will fail:
        //1. The component under test (#getPhotos element)'s text changes, then expected value needs to be changed in the test

        //This test will not fail if:
        //1. Number of photos may change in the database and the test will fail. 
        //2. Database server is down or table is modified.
        //3. API server is down or API is modified.
        //4. The base address of the API is changed.

        private IRenderedComponent<TestExample1> createTestExample1Component() {
            var photoService = new ServiceDescriptor(typeof(IPhotoService), new MockPhotoService());
            Services.Add(photoService);
            var cut = RenderComponent<TestExample1>();
            return cut;
        }

        [Fact(DisplayName = "3 photos when you click on 'get photos' button")]
        public void TestGetPhotosButton()
        {
            //Arrange
            var cut = createTestExample1Component();

            //Act
            cut.Find("#getPhotos").Click();
            var text = cut.Find("#countOfPhoto").TextContent;

            //Assert
            text.MarkupMatches("Count of photos: 3");
        }

        [Fact(DisplayName = "Clear button clears photos")]
        public void TestClearButton()
        {
            //Arrange
            var cut = createTestExample1Component();

            //Act
            cut.Find("#getPhotos").Click();
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
