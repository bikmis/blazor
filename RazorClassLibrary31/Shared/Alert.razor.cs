using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Shared
{
    public partial class Alert : IDisposable
    {
        [Inject]
        private IAppStateService appStateService { get; set; }

        public void Dispose()
        {
            appStateService.OnChange -= StateHasChanged;
        }

        protected override Task OnInitializedAsync()
        {
            appStateService.OnChange += StateHasChanged;
            return base.OnInitializedAsync();
        }

        private void closeMessage()
        {
            appStateService.AlertPopUp = new AlertPopUp() { IsHidden = true };
        }

    }
}
