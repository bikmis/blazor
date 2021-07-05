using Intel.EmployeeManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Intel.EmployeeManagement.Data
{
    public class EmployeeDbContext : DbContext
    {
        //The following constructor is used by IdentityProvider and WebAPI, and is useful for testing with dependency injection
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) {            
        }       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Add reference to Microsoft.EntityFrameworkCore.SqlServer from NuGet packages to use "UseSqlServer" method on DbContextOptionsBuilder 
            //Migrations are done from IdentityProvider service
            optionsBuilder.UseSqlServer(b => b.MigrationsAssembly("Intel.EmployeeManagement.IdentityProvider"));
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                ID = 1,
                FirstName = "John",
                MiddleName = "",
                LastName = "Smith",
                DateOfBirth = new DateTime(1980, 02, 15),
                Position = "Sales Rpresentative",
                DepartmentID = 5,
                Gender = 'M'
            },
            new Employee
            {
                ID = 2,
                FirstName = "Jack",
                MiddleName = "",
                LastName = "Jackson",
                DateOfBirth = new DateTime(1985, 03, 17),
                Position = "Operations Manager",
                DepartmentID = 10,
                Gender = 'M'
            },
             new Employee
             {
                 ID = 3,
                 FirstName = "Jane",
                 MiddleName = "Williams",
                 LastName = "Ferguson",
                 DateOfBirth = new DateTime(1987, 06, 12),
                 Position = "Operations Manager",
                 DepartmentID = 15,
                 Gender = 'F'
             },
             new Employee
             {
                 ID = 4,
                 FirstName = "Leo",
                 MiddleName = "",
                 LastName = "Wilson",
                 DateOfBirth = new DateTime(1978, 10, 25),
                 Position = "Head Chef",
                 DepartmentID = 20,
                 Gender = 'M'
             }
             ,
             new Employee
             {
                 ID = 5,
                 FirstName = "Harry",
                 MiddleName = "",
                 LastName = "Burton",
                 DateOfBirth = new DateTime(1974, 12, 27),
                 Position = "CEO",
                 DepartmentID = 15,
                 Gender = 'M'
             });

            modelBuilder.Entity<Role>().HasData(new Role()
            {
                ID = 1,
                UserID = 1,
                RoleName = "hr"
            }, new Role() {
                ID = 2,
                UserID = 2,
                RoleName = "hr"
            },
            new Role() {
                ID = 3,
                UserID = 3,
                RoleName = "data entry"
            },
            new Role() {
                ID = 4,
                UserID = 1,
                RoleName = "admin"
            });

            modelBuilder.Entity<User>().HasData(new User()
            {
                ID = 1,
                Username = "bikash@gmail.com",
                Password = "password",
                Email = "bikash@gmail.com"
            },
            new User()
            {
                ID = 2,
                Username = "jack@gmail.com",
                Password = "password",
                Email = "jack@gmail.com"
            },
            new User()
            {
                ID = 3,
                Username = "mike@gmail.com",
                Password = "password",
                Email = "mike@gmail.com"
            });
        }
    }
}


