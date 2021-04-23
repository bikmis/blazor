using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Route("employees")]
        public List<string> GetEmployees() {
            EmployeeDbContext dbContext = new EmployeeDbContext();
            var employees = dbContext.Employees.ToList().Select(e => new { e.FirstName }.ToString()).ToList();

            return employees;
        }

    }
}
