using Intel.EmployeeManagement.Data;
using Intel.EmployeeManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Intel.EmployeeManagement.EmployeeWebApi.IntegrationTests_2
{
    public class SharedService
    {
        public static EmployeeDbContext ProvideEmployeeDbContextWithRealDatabase()
        {
            var configuration = ProvideConfiguration();
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseSqlServer(configuration.GetConnectionString("EmployeeDb")).Options;
            var employeeDbContext = new EmployeeDbContext(options);
            return employeeDbContext;
        }

        public static IConfigurationRoot ProvideConfiguration()
        {
            var configurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return configurationRoot;
        }
    }
}
