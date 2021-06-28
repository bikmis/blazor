using Bunit;
using Intel.EmployeeManagement.BlazorClient.Tests.Unit_Tests.Services;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests.Unit_Tests
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

        [Fact(DisplayName = "Jack, Mike and Sophia are displayed when you land on the page")]
        public void TestComponentCreation()
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

        [Fact(DisplayName = "When Mike is searched, only Mike is found")]
        public void TestSearchTextBox()
        {
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

        [Fact(DisplayName = "Edit form is populated with data when edit button is clicked")]
        public void TestEditButton()
        {
            //Arrange
            var cut = createEmployeeListComponent();

            //Act - before edit button is clicked
            var firstName = cut.Find("#firstName");

            //Assert
            firstName.MarkupMatches("<input id='firstName' class='form-control valid' />");

            //Act - edit button is clicked
            var editButton = cut.Find("#edit-1");
            editButton.Click();
            firstName = cut.Find("#firstName");

            //Assert
            firstName.MarkupMatches("<input id='firstName' class='form-control valid' value='Jack' />");
        }
        
        [Fact(DisplayName = "When delete button is clicked, Jack's record is deleted and an alert message shows up")]
        public void TestDeleteButton() {
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
        
        [Fact(DisplayName = "When Save button is clicked on edit form, middle name Thomas is added and an alert pop up shows up")]
        public void SubmitEditForm() {
            //Arrange
            var cut = createEmployeeListComponent();
            JSInterop.SetupVoid("closeEmployeeEditModal").SetVoidResult();

            //Act - before form submit
            var editButton = cut.Find("#edit-1");
            editButton.Click();
            var alertPopup = cut.Find("#alert-popup");
            var isAlertHidden = alertPopup.HasAttribute("hidden");
            var markup = cut.Markup;
            var middleNameExists = markup.Contains("<td>Thomas</td>");

            //Assert
            Assert.True(isAlertHidden);
            Assert.False(middleNameExists);

            //Act
            var editForm = cut.Find("#edit-form");
            editForm.Submit();
            isAlertHidden = alertPopup.HasAttribute("hidden");
            markup = cut.Markup;
            middleNameExists = markup.Contains("<td>Thomas</td>");

            //Assert
            Assert.False(isAlertHidden);
            Assert.True(middleNameExists);
        }
        
    }
}
