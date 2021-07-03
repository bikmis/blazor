using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service
{
    public interface IAuthenticationService
    {
        Task<HttpResponseMessage> LoginUser(Login login);
        void LogoutUser();
        Task GuardRoute();
        Task<HttpResponseMessage> GetAccessToken();
    }
}
