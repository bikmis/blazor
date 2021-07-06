using Intel.EmployeeManagement.Data;
using Intel.EmployeeManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Intel.EmployeeManagement.Tests.Identity_Provider_Unit_Tests.Services
{
    public class LoginService
    {
        public static EmployeeDbContext ProvideEmployeeDbContextWithInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var employeeDbContext = new EmployeeDbContext(options);

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

        public static IConfigurationRoot ProvideConfiguration() {
            var configurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return configurationRoot;
        }
    }
}
