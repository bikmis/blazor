using Intel.EmployeeManagement.WebAPI.Controllers;
using Intel.EmployeeManagement.WebAPI.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Intel.EmployeeManagement.EmployeeWebApi.IntegrationTests_2
{
    public class EmployeeControllerTests
    {
        [Fact(DisplayName = "Employee with ID 1 is John")]
        public void TestGetEmployeeById()
        {
            //Arrange
            var employeeController = arrangeEmployeeController();

            //Act
            var response = employeeController.GetEmployee(1);
            var employeeResponse = (EmployeeResponse)(response as OkObjectResult).Value;

            //Asert
            Assert.True(employeeResponse.FirstName == "John");
        }

        private EmployeeController arrangeEmployeeController()
        {
            var dbContext = SharedService.ProvideEmployeeDbContextWithRealDatabase();
            var employeeController = new EmployeeController(dbContext);
            return employeeController;
        }
    }
}
