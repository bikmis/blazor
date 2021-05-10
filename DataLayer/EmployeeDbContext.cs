using DataLayer31.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DataLayer31
{
    public class EmployeeDbContext : DbContext
    {
        private string _connectionString = null;
        public EmployeeDbContext()
        {
            //To read configuration string from appsettings.json, add the NuGet package "Microsoft.Extensions.Configuration.Json".
            //This constructor runs when you set this as Startup project and run migration commands (PM>add-migration ...migration..name, PM>update-database)
            //as we are using the appsettings.json from this project. The appsettings.json file is used with DataLayer project to run migration commands
            //independently of other projects. This appsettings.json is not used in production and it is only used during development and is replaced by
            //another appsettings.json from web api project (or mvc or blazor etc). That's why appsettings.json of this project will have the value "Do not copy"
            //for "Copy to Output Directory" property. To access this property, right-click on appsettings.json and the appsettings.json file with a connection
            //string from web api (or mvc or blazor etc) will have the property "Copy to Output Directory" set to "Copy if newer".
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetConnectionString("EmployeeDb");
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Add reference to Microsoft.EntityFrameworkCore.SqlServer from NuGet packages to use "UseSqlServer" method on DbContextOptionsBuilder 
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                ID = 1,
                FirstName = "John",
                MiddleName = "",
                LastName = "Smith",
                DateOfBirth = new DateTime(1980, 02, 15),
                Position = "Sales Rpresentative"
            }, 
            new Employee { 
                ID = 2,
                FirstName = "Jack",
                MiddleName = "",
                LastName = "Jackson",
                DateOfBirth = new DateTime(1985, 03,17),
                Position = "Operations Manager"
            });
        }

    }
}


