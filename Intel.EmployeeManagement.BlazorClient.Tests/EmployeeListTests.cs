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

        [Fact]
        public void When_Mike_is_searched_only_Mike_is_found() {
            //Arrange
            var cut = createEmployeeListComponent();

            //No search
            //Act
            var markup = cut.Markup;
            var jackExists = markup.Contains("Jack");
            var mikeExists = markup.Contains("Mike");
            var sophiaExists = markup.Contains("Sophia");

            //Assert            
            Assert.True(jackExists);
            Assert.True(mikeExists);
            Assert.True(sophiaExists);

            //Search begins
            //Act
            var searchElement = cut.Find("#search");
            searchElement.Input("Mike");
            searchElement.KeyUp(Key.Up);
            markup = cut.Markup;
            jackExists = markup.Contains("Jack");
            mikeExists = markup.Contains("Mike");
            sophiaExists = markup.Contains("Sophia");

            //Assert
            Assert.False(jackExists);
            Assert.True(mikeExists);
            Assert.False(sophiaExists);
        }

        /*
        [Fact]
        public void When_edit_is_clicked_modal_is_displayed() {
            //Arrange
            var cut = createEmployeeListComponent();
            var employeeEditComponent = RenderComponent<EmployeeEdit>();

            //Act - before edit button is clicked
            var markup = employeeEditComponent.Markup;
            var modalIsDisplayed = markup.Contains("display: block;");

            Assert.False(modalIsDisplayed);

            //Act - edit button is clicked
            var editButton = cut.Find("#edit-1");
            editButton.Click();
            markup = employeeEditComponent.Markup;
            modalIsDisplayed = markup.Contains("display: block;");

            //Assert
            Assert.True(modalIsDisplayed);
        }
        */

        [Fact]
        public void When_delete_is_clicked_Jack_is_removed_and_message_shows_up() {
            //Arrange
            var cut = createEmployeeListComponent();

            //Act - before delete button is clicked
            var markup = cut.Markup;
            var JackIsDisplayed = markup.Contains("Jack");
            var alertPopup = cut.Find("#alert-popup");
            var isAlertHidden = alertPopup.HasAttribute("hidden");

            //Assert
            Assert.True(JackIsDisplayed);
            Assert.True(isAlertHidden);

            //Act - afer delete button is clicked
            var deleteButton = cut.Find("#delete-1");
            deleteButton.Click();
            markup = cut.Markup;
            JackIsDisplayed = markup.Contains("Jack");
            isAlertHidden = alertPopup.HasAttribute("hidden");

            //Assert
            Assert.False(JackIsDisplayed);
            Assert.False(isAlertHidden);
        }

    }
}
