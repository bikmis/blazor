using Intel.EmployeeManagement.Data;
using Intel.EmployeeManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Intel.EmployeeManagement.Tests
{
    public class SharedService
    {
        public static EmployeeDbContext ProvideEmployeeDbContextWithInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var employeeDbContext = new EmployeeDbContext(options);

            List<Employee> employees = new List<Employee>()
            {
                new Employee(){ ID = 1, FirstName = "Jack", MiddleName = "", LastName = "Smith", DateOfBirth = new DateTime(1985,5,15), DepartmentID = 1, Gender = 'M', Position = "CEO" },
                new Employee(){ ID = 2, FirstName = "Mike", MiddleName = "", LastName = "Burton", DateOfBirth = new DateTime(1981,4,13), DepartmentID = 5, Gender = 'M', Position = "Programmer" },
                new Employee(){ ID = 3, FirstName = "Marie", MiddleName = "", LastName = "Brown", DateOfBirth = new DateTime(1979,9,24), DepartmentID = 10, Gender = 'F', Position = "Head Chef" }
            };
            employeeDbContext.Employees.AddRange(employees);

            List<User> users = new List<User>() {
                new User() { ID=1, Username="bikash@gmail.com", Password="password", Email="bikash@gmail.com"},
                new User() { ID=2, Username="jack@gmail.com", Password="password", Email="jack@gmail.com"}
            };
            employeeDbContext.Users.AddRange(users);

            List<Role> roles = new List<Role>()
            {
                new Role() { ID = 1, UserID = 1, RoleName = "hr"},
                new Role() { ID = 2, UserID = 2, RoleName = "data entry"}
            };
            employeeDbContext.Roles.AddRange(roles);

            employeeDbContext.SaveChanges();
            return employeeDbContext;
        }

        public static IConfigurationRoot ProvideConfiguration()
        {
            var configurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return configurationRoot;
        }
    }
}
