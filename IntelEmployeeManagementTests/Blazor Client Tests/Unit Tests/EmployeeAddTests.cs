using Bunit;
using Intel.EmployeeManagement.RazorClassLibrary.Pages.EmployeePage;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using Intel.EmployeeManagement.Tests.Blazor_Client_Tests.Unit_Tests.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Intel.EmployeeManagement.Tests.Blazor_Client_Tests.Unit_Tests
{
    public class EmployeeAddTests : TestContext
    {
        [Fact(DisplayName = "When you submit Add Employee form, an employee is added and an alert message shows up")]
        public void AddEmployee()
        {
            //Arrange
            var appStateService = new ServiceDescriptor(typeof(IAppStateService), new MockAppStateService());
            Services.Add(appStateService);
            var employeeService = new ServiceDescriptor(typeof(IEmployeeService), new MockEmployeeService());
            Services.Add(employeeService);
            var authenticationStateProvider = new ServiceDescriptor(typeof(AuthenticationStateProvider), new MockAuthenticationService());
            Services.Add(authenticationStateProvider);
            var authenticationService = new ServiceDescriptor(typeof(IAuthenticationService), new MockAuthenticationService());
            Services.Add(authenticationService);
            var cutEmployeeAdd = RenderComponent<EmployeeAdd>();

            //Act
            var firstnameField = cutEmployeeAdd.Find("#firstName");
            firstnameField.Change("Hazel");
            var middleNameField = cutEmployeeAdd.Find("#middleName");
            middleNameField.Change("");
            var lastnameField = cutEmployeeAdd.Find("#lastName");
            lastnameField.Change("Green");
            var departmentIdField = cutEmployeeAdd.Find("#departmentID");
            departmentIdField.Change("15");
            var genderField = cutEmployeeAdd.Find("#female-gender");
            genderField.Change("F");
            var positionField = cutEmployeeAdd.Find("#position");
            positionField.Change("CFO");
            var dateOfBirthField = cutEmployeeAdd.Find("#dateOfBirth");
            dateOfBirthField.Change(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            var employeeAddForm = cutEmployeeAdd.Find("#employee-add-form");
            employeeAddForm.Submit();

            //Go to EmployeeList component
            var cutEmployeeList = RenderComponent<EmployeeList>();
            var markup = cutEmployeeList.Markup;
            var hazelExists = markup.Contains("Hazel");

            //Assert
            Assert.True(hazelExists);

            //Act
            var alertPopup = cutEmployeeList.Find("#alert-popup");
            var isAlertPopupVisible = !alertPopup.HasAttribute("hidden");  //when visible, hidden does not exist on the element

            //Assert
            Assert.True(isAlertPopupVisible);           
        }

    }
}
