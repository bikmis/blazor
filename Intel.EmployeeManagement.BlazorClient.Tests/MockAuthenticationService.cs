using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.BlazorClient.Tests
{
    public class MockAuthenticationService : AuthenticationStateProvider, IAuthenticationService
    {
        public Task<HttpResponseMessage> GetAccessToken()
        {
            throw new NotImplementedException();
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }

        public Task GuardRoute()
        {
            return Task.CompletedTask;
        }

        public Task<HttpResponseMessage> LoginUser(Login login)
        {
            throw new NotImplementedException();
        }

        public void LogoutUser()
        {
            throw new NotImplementedException();
        }
    }
}
