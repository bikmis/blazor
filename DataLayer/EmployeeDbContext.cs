using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DataLayer
{
    public class EmployeeDbContext : DbContext
    {
        private string _connectionString = null;
        public EmployeeDbContext()
        {
            //To read configuration string from appsettings.json, add the NuGet package "Microsoft.Extensions.Configuration.Json"
            //This constructor runs when you set this as startup project and run PM>add-migration ...name and PM>update-database
            //appsettings.json of this project will have the value "Do not copy" for "Copy to Output Directory"
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


