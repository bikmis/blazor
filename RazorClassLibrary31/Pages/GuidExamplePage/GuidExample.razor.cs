using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Guid_Service;
using System;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.GuidExamplePage
{
    public partial class GuidExample
    {
        [Inject]
        private IGuidServiceSingleton guidServiceSingleton { get; set; }

        [Inject]
        private IGuidServiceScoped guidServiceScoped { get; set; }

        [Inject]
        private IGuidServiceTransient guidServiceTransient { get; set; }

        private Guid guidSingleton { get; set; }

        private int counterSingleton { get; set; }

        private Guid guidScoped { get; set; }

        private int counterScoped { get; set; }

        private Guid guidTransient { get; set; }

        private int counterTransient { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
            createSingleton();
            createScoped();
            createTransient();
        }

        private void createSingleton()
        {
            guidSingleton = guidServiceSingleton.CreateGuid();
            counterSingleton = guidServiceSingleton.Increment();
            StateHasChanged();
        }

        private void createScoped()
        {
            guidScoped = guidServiceScoped.CreateGuid();
            counterScoped = guidServiceScoped.Increment();
            StateHasChanged();
        }

        private void createTransient()
        {
            guidTransient = guidServiceTransient.CreateGuid();
            counterTransient = guidServiceTransient.Increment();
            StateHasChanged();
        }

    }
}
