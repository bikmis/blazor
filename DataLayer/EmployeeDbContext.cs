using Intel.EmployeeManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Intel.EmployeeManagement.Data
{
    public class EmployeeDbContext : DbContext
    {
        private string _connectionString = null;
        public EmployeeDbContext()
        {
            //To read configuration string from appsettings.json, add the NuGet package "Microsoft.Extensions.Configuration.Json".

            //1. This constructor runs when you set Default project to this project and run migration commands
            //(PM>add-migration ...migration..name, PM>update-database), then the appsettings.json from this project will be used.
            //2. This project is referenced by IdentityProvider project, and when a call is made to this from there, appsettings.json of IdentityProvider is used.
            //3. This project is referenced by WebApi project, and when a call is made to this from there, appsettings.json of WebApi is used.
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetConnectionString("EmployeeDb");
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Logs { get; set; }

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


