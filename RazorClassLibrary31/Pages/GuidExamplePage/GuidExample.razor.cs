using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Intel.Personnel.RazorClassLibrary.Services.Authentication_Service;
using Intel.Personnel.RazorClassLibrary.Services.Guid_Service;
using System;
using System.Threading.Tasks;

namespace Intel.Personnel.RazorClassLibrary.Pages.GuidExamplePage
{
    public partial class GuidExample
    {
        [Inject]
        private IGuidServiceAddSingleton guidServiceAddSingleton { get; set; }

        [Inject]
        private IGuidServiceAddScoped guidServiceAddScoped { get; set; }

        [Inject]
        private IGuidServiceAddTransient guidServiceAddTransient { get; set; }

        private Guid guidAddSingleton { get; set; }

        private int counterAddSingleton { get; set; }

        private Guid guidAddScoped { get; set; }

        private int counterAddScoped { get; set; }

        private Guid guidAddTransient { get; set; }

        private int counterAddTransient { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
            addSingleton();
            addScoped();
            addTransient();
        }

        private void addSingleton()
        {
            guidAddSingleton = guidServiceAddSingleton.CreateGuid();
            counterAddSingleton = guidServiceAddSingleton.Increment();
            StateHasChanged();
        }

        private void addScoped()
        {
            guidAddScoped = guidServiceAddScoped.CreateGuid();
            counterAddScoped = guidServiceAddScoped.Increment();
            StateHasChanged();
        }

        private void addTransient()
        {
            guidAddTransient = guidServiceAddTransient.CreateGuid();
            counterAddTransient = guidServiceAddTransient.Increment();
            StateHasChanged();
        }

    }
}
