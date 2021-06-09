using Intel.EmployeeManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.WebAPI.Services.Database_Service
{
    public class DatabaseService : IDatabaseService
    {
        private EmployeeDbContext _employeeDbContext { get; set; }

        public DatabaseService()
        {
            _employeeDbContext = new EmployeeDbContext();
        }

        public EmployeeDbContext EmployeeDbContext {  get { return _employeeDbContext; } }
    }
}
