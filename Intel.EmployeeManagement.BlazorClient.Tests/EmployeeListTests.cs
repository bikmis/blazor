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
        [Fact]
        public void Three_employees_are_displayed_when_you_land_on_this_page()
        {
            //Arrange
            var appStateService = new ServiceDescriptor(typeof(IAppStateService), new MockAppStateService());
            var employeeService = new ServiceDescriptor(typeof(IEmployeeService), new MockEmployeeService());
            var authenticationStateProviderService = new ServiceDescriptor(typeof(AuthenticationStateProvider), new MockAuthenticationService());
            var authenticationService = new ServiceDescriptor(typeof(IAuthenticationService), new MockAuthenticationService());
            Services.Add(authenticationStateProviderService);
            Services.Add(authenticationService);
            Services.Add(appStateService);
            Services.Add(employeeService);
            var cut = RenderComponent<EmployeeList>();

            //Act
            var markup = cut.Markup;
            var sophiaExists = markup.Contains("Jack");
            var millerExists = markup.Contains("Mike");
            var jackExists = markup.Contains("Sophia");

            //Asert
            Assert.True(sophiaExists);
            Assert.True(millerExists);
            Assert.True(jackExists);
        }
    }
}
