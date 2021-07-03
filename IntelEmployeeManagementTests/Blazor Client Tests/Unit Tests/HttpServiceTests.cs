using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using Intel.EmployeeManagement.Tests.Blazor_Client_Tests.Unit_Tests.Services;
using System.Net.Http;
using Xunit;

namespace Intel.EmployeeManagement.Tests.Blazor_Client_Tests.Unit_Tests
{
    public class HttpServiceTests
    {
        [Fact]
        public async void TestHttpService() {
            //Arrange
            var appStateService = new MockAppStateService();
            var authenticationService = new MockAuthenticationService();
            var httpClient = new HttpClient();

            //Act
            var httpService = new HttpService(appStateService, authenticationService, httpClient);
            var response = await httpService.SendAsync(HttpMethod.Get, "https://jsonplaceholder.typicode.com/photos", null);
            var photos = await appStateService.DeserializeToList<Photo>(response);

            //Assert
            Assert.True(photos.Count == 5000);
        }

    }
}
