using Intel.EmployeeManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.IdentityProvider.Services.Database_Service
{
    public class DatabaseService : IDatabaseService
    {
        EmployeeDbContext employeeDbContext { get; set; }

        public DatabaseService()
        {
            employeeDbContext = new EmployeeDbContext();
        }

        public EmployeeDbContext EmployeeDbContext { get => employeeDbContext; }
    }
}
