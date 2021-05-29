using RazorClassLibrary31.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.LoginService
{
    public interface ILoginService
    {
        Task<bool> LoginUser(Login login);
        void LogoutUser(User user);
    }
}
