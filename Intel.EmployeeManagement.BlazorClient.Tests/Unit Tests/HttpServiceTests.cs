using Intel.EmployeeManagement.BlazorClient.Tests.Unit_Tests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests.Unit_Tests
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
