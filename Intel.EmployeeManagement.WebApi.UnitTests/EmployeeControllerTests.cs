using Intel.EmployeeManagement.WebAPI.Controllers;
using Intel.EmployeeManagement.WebAPI.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;


namespace Intel.EmployeeManagement.WebApi.UnitTests
{
    public class EmployeeControllerTests
    {
        [Fact(DisplayName = "Number of employees is 3")]
        public void TestGetEmployees()
        {
            //Arrange
            var employeeController = arrangeEmployeeController();

            //Act
            var response = employeeController.GetEmployees();
            var employeeResponse = (List<EmployeeResponse>)(response as OkObjectResult).Value;

            //Assert
            Assert.True(employeeResponse.Count == 3);
        }

        [Fact(DisplayName = "Employee with ID 1 is Jack")]
        public void TestGetEmployee() {
            //Arrange
            var employeeController = arrangeEmployeeController();

            //Act
            var response = employeeController.GetEmployee(1);
            var employeeResponse = (EmployeeResponse)(response as OkObjectResult).Value;

            //Asert
            Assert.True(employeeResponse.FirstName == "Jack");
        }

        [Fact(DisplayName = "Employee with ID 10 is not found")]
        public void TestGetEmployeeNotFound()
        {
            //Arrange
            var employeeController = arrangeEmployeeController();

            //Act
            var response = employeeController.GetEmployee(10);
            var employeeResponse = (response as NotFoundObjectResult).Value;

            //Asert
            Assert.True(employeeResponse.ToString() == "Employee not found.");
        }

        [Fact(DisplayName = "After you add employee, count of employee is 4 and status code is 200")]
        public void TestAddEmployee()
        {
            //Arrange
            var employeeController = arrangeEmployeeController();
            EmployeeRequest employeeRequest = new EmployeeRequest() { ID = 4, FirstName = "Test_FirstName", MiddleName = "Test_MiddleName", LastName = "Test_LastName", DateOfBirth = new DateTime(1980, 5, 16), DepartmentID = 10, Gender = 'M', Position = "Test_Position" };

            //Act
            var response = employeeController.AddEmployee(employeeRequest);
            var responseStatusCode = (response as OkResult).StatusCode;
            var getEmployeesResponse = employeeController.GetEmployees();
            var employeesResponse = (List<EmployeeResponse>)(getEmployeesResponse as OkObjectResult).Value;

            //Asert
            Assert.True(employeesResponse.Count == 4);
            Assert.True(responseStatusCode == 200);
        }

        [Fact(DisplayName = "After you delete employee, count of employee is 2 and status code is 200")]
        public void TestDeleteEmployee()
        {
            //Arrange
            var employeeController = arrangeEmployeeController();

            //Act
            var response = employeeController.DeleteEmployee(1);
            var responseStatusCode = (response as OkResult).StatusCode;
            var getEmployeesResponse = employeeController.GetEmployees();
            var employeesResponse = (List<EmployeeResponse>)(getEmployeesResponse as OkObjectResult).Value;

            //Asert
            Assert.True(employeesResponse.Count == 2);
            Assert.True(responseStatusCode == 200);
        }

        [Fact(DisplayName = "Update employee with ID 1 to junk values")]
        public void TestUpdateEmployee()
        {
            //Arrange
            var employeeController = arrangeEmployeeController();
            EmployeeRequest employeeRequest = new EmployeeRequest() { ID = 1, FirstName = "Test_FirstName", MiddleName = "Test_MiddleName", LastName = "Test_LastName", DateOfBirth = new DateTime(1980, 5, 16), DepartmentID = 10, Gender = 'M', Position = "Test_Position" };

            //Act
            var response = employeeController.UpdateEmployee(employeeRequest);
            var responseStatusCode = (response as OkResult).StatusCode;
            var getEmployeeResponse = employeeController.GetEmployee(1);
            var employeeResponse = (EmployeeResponse)(getEmployeeResponse as OkObjectResult).Value;

            //Asert
            Assert.True(employeeResponse.FirstName == "Test_FirstName");
            Assert.True(responseStatusCode == 200);
        }

        private EmployeeController arrangeEmployeeController() {
            var dbContext = SharedService.ProvideEmployeeDbContextWithInMemoryDatabase();
            var employeeController = new EmployeeController(dbContext);
            return employeeController;
        }
    }
}
