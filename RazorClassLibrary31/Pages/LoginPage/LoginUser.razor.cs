using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.LoginPage
{
    public partial class LoginUser
    {
        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        private Login login { get; set; } = new Login();

        private string error { get; set; }

        private bool isLoginInProgress { get; set; }

        protected override Task OnInitializedAsync()
        {
            isLoginInProgress = false;
            return base.OnInitializedAsync();
        }

        private async Task loginUser()
        {
            try
            {
                isLoginInProgress = true;
                var response = await ((AuthenticationService)authenticationService).LoginUser(login);
                if (!response.IsSuccessStatusCode) {
                    isLoginInProgress = false;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        error = "Check your username and password.";
                    }
                    else
                    {
                        error = $"Response status code is { (int)response.StatusCode} {response.StatusCode}";
                    }
                }
            }
            catch (Exception e)
            {
                isLoginInProgress = false;
                error = e.Message;
                await _jsRuntime.InvokeVoidAsync("writeToConsole", e.Message); //This works with both Blazor web assembly and Blazor server.
                Console.WriteLine(e.Message);  //writes to the browser console in web assembly only, does not work for Blazor server.
            }
        }
    }
}
