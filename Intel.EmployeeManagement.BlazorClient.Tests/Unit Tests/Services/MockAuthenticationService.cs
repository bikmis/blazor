using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.BlazorClient.Tests.Unit_Tests.Services
{
    public class MockAuthenticationService : AuthenticationStateProvider, IAuthenticationService
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> LoginUser(Login login)
        {
            throw new NotImplementedException();
        }

        public void LogoutUser()
        {
            throw new NotImplementedException();
        }

        public Task GuardRoute()
        {
            return Task.CompletedTask;
        }

        public Task<HttpResponseMessage> GetAccessToken()
        {
            throw new NotImplementedException();
        }
    }
}
