using Razor.Components.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Razor.Components.Library.Services.UserService
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsers();
    }
}
