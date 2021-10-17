using Intel.EmployeeManagement.WebAPI.IntegrationTestsQaEnv.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Xunit;

namespace Intel.EmployeeManagement.WebAPI.IntegrationTestsQaEnv
{
    public class EmployeeControllerTests
    {
        private string webApiTestSiteBaseUrl = Configuration.Build().GetSection("WebApiTestSiteBaseUrl").Value;

        [Fact(DisplayName = "Get Employees")]
        public void TestGetEmployees()
        {
            var token = Token.Get();
            var client = new HttpClient() { BaseAddress = new Uri(webApiTestSiteBaseUrl) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var employeeResponse = client.GetAsync("api/employees").Result.Content.ReadAsStringAsync().Result;
            var employees = JsonSerializer.Deserialize<List<Employee>>(employeeResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            Assert.True(employees.Count > 0);
        }
    }
}
