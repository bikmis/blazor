using RazorClassLibrary31.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
