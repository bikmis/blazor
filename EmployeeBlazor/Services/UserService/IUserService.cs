using EmployeeBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeBlazor.Services.UserService
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsers();
    }
}
