using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EmployeeWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeDbContext _dbContext;

        public EmployeeController()
        {
            _dbContext = new EmployeeDbContext();
        }

        [Route("employees")]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _dbContext.Employees.ToList();
            return Ok(employees);
        }

        [Route("employee/{id}")] //{id} is a variable parameter
        [HttpGet]
        public IActionResult GetEmployee(int id) {
            var employee = _dbContext.Employees.Find(id);
            return Ok(employee);
        }

        [Route("employee/add")]
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _dbContext.Add(employee);
            _dbContext.SaveChanges();
            return Ok();
        }

        [Route("employee/delete")]
        [HttpPost]
        public IActionResult DeleteEmployee(int id) {
            var employee = _dbContext.Employees.Find(id);
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return Ok();
        }


        [Route("employee/update")]
        [HttpPost]
        public IActionResult UpdateEmployee(int id, string firstName, string middleName, string lastName, DateTime dateOfBirth, string position) {
            var employee = _dbContext.Employees.Find(id);
            employee.FirstName = firstName;
            employee.MiddleName = middleName;
            employee.LastName = lastName;
            employee.DateOfBirth = dateOfBirth;
            employee.Position = position;
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
