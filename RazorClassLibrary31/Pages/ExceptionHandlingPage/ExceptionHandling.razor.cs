using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.ExceptionService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.ExceptionHandlingPage
{
    public partial class ExceptionHandling
    {
        [Inject]
        private IDivideByZeroService divideByZeroService { get; set; }

        [Inject]
        private IAppStateService appStateService { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }


        protected async override Task OnInitializedAsync()
        {
            await((AuthenticationService)authenticationService).GuardRoute();
        }

        private async Task divideByZero()
        {
            try
            {
                var response = await divideByZeroService.DivideByZero();
                if (!response.IsSuccessStatusCode)
                {
                    appStateService.AlertPopUp = new AlertPopUp() { Message = $"{(int)response.StatusCode} {response.ReasonPhrase}", IsHidden = false, Color = "alert-danger" };
                }
            }
            catch (Exception e)
            {
                appStateService.AlertPopUp = new AlertPopUp() { Message = e.Message, IsHidden = false, Color = "alert-danger" };
            }
        }
    }
}
