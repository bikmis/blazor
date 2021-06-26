using Bunit;
using Intel.EmployeeManagement.BlazorClient.Tests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests
{
    public class EmployeeListTests : TestContext
    {
        private IRenderedComponent<EmployeeList> createEmployeeListComponent() {
            var appStateService = new ServiceDescriptor(typeof(IAppStateService), new MockAppStateService());
            Services.Add(appStateService);
            var employeeService = new ServiceDescriptor(typeof(IEmployeeService), new MockEmployeeService());
            Services.Add(employeeService);
            var authenticationStateProviderService = new ServiceDescriptor(typeof(AuthenticationStateProvider), new MockAuthenticationService());
            Services.Add(authenticationStateProviderService);
            var authenticationService = new ServiceDescriptor(typeof(IAuthenticationService), new MockAuthenticationService());
            Services.Add(authenticationService);
            var cut = RenderComponent<EmployeeList>();
            return cut;
        }

        [Fact]
        public void Three_employees_are_displayed_when_you_land_on_this_page()
        {
            //Arrange
            var cut = createEmployeeListComponent();

            //Act
            var markup = cut.Markup;
            var jackExists = markup.Contains("Jack");
            var mikeExists = markup.Contains("Mike");
            var sophiaExists = markup.Contains("Sophia");

            //Assert
            Assert.True(jackExists);
            Assert.True(mikeExists);
            Assert.True(sophiaExists);
        }
/*
        [Fact]
        public void Only_mike_is_searched() {
            //Arrange
            var cut = createEmployeeListComponent();

            //Act
            var markup = cut.Markup;
            var jackExists = markup.Contains("Jack");
            var mikeExists = markup.Contains("Mike");
            var sophiaExists = markup.Contains("Sophia");

            //Assert            
            Assert.True(jackExists);
            Assert.True(mikeExists);
            Assert.True(sophiaExists);

            //Act
            var searchElement = cut.Find("#search");
           // searchElement.TextContent = "Mike";
            searchElement.KeyUp("Mike".ToString());
            markup = cut.Markup;
            jackExists = markup.Contains("Jack");
            mikeExists = markup.Contains("Mike");
            sophiaExists = markup.Contains("Sophia");

            //Assert
            Assert.False(jackExists);
            Assert.True(mikeExists);
            Assert.False(sophiaExists);
        }
*/
    }
}
