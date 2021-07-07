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

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData.Populate(modelBuilder);
        }
    }
}


