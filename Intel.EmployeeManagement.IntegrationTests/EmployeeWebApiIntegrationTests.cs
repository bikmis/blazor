using Intel.EmployeeManagement.WebAPI;
using Intel.EmployeeManagement.WebAPI.Models.Employee;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Intel.EmployeeManagement.IntegrationTests
{
    //https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0
    //At the above site, check "Test app prerequisites" which states that the project should have "<Project Sdk="Microsoft.NET.Sdk.Web">" instead of "<Project Sdk="Microsoft.NET.Sdk">" i.e. specify the Web SDK
    //and reference the Microsoft.AspNetCore.Mvc.Testing package.
    public class EmployeeWebApiIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public EmployeeWebApiIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Theory(DisplayName = "Test GetEmployee by id")]
        [InlineData(1)]
        public async Task Test_GetEmployeeById(int id)
        {
            //Arrange
            var client = factory.CreateClient();

            //Act
            var response = await client.GetAsync($"api/employees/{id}");
            var employeeResponse = JsonSerializer.Deserialize<EmployeeResponse>(response.Content.ReadAsStringAsync().Result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            //Assert
            Assert.True(employeeResponse.FirstName == "John");
        }
    }
}
