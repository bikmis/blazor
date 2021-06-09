using Intel.EmployeeManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.WebAPI.Services.Database_Service
{
    public interface IDatabaseService
    {
        EmployeeDbContext EmployeeDbContext { get; }
    }
}
