using Intel.EmployeeManagement.WebAPI.Controllers;
using Intel.EmployeeManagement.WebAPI.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace Intel.EmployeeManagement.Tests.Employee_Web_API_Tests.Unit_Tests
{
    public class EmployeeControllerTests
    {
        [Fact(DisplayName = "Number of employees is 3")]
        public void TestGetEmployees()
        {
            //Arrange
            var dbContext = SharedService.ProvideEmployeeDbContextWithInMemoryDatabase();
            var employeeController = new EmployeeController(dbContext);

            //Act
            var response = employeeController.GetEmployees();
            var responseObject = (List<EmployeeResponse>)(response as OkObjectResult).Value;

            //Assert
            Assert.True(responseObject.Count == 3);
        }
    }
}
