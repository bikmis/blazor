using Bunit;
using Intel.EmployeeManagement.BlazorClient.UnitTests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.CounterPage;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.UnitTests
{
    public class CounterTests : TestContext
    {
        [Fact(DisplayName = "Counter will increment when you click on the button")]
        public void TestIncrementCounter()
        {
            var authenticationStateProviderService = new ServiceDescriptor(typeof(AuthenticationStateProvider), new MockAuthenticationService());
            var authenticationService = new ServiceDescriptor(typeof(IAuthenticationService), new MockAuthenticationService());
            Services.Add(authenticationStateProviderService);
            Services.Add(authenticationService);

            //Arrange: render the Counter.razor component (cut means component under test)
            var cut = RenderComponent<Counter>();

            //Act: find and click the <button> element to increment
            cut.Find("button").Click();

            //Assert: first find the <p> element, then verify its content
            cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
        }
    }
}
