using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Guid_Service;
using System;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.GuidTwoPage 
{
    public partial class GuidTwo
    {
        [Inject]
        private ISingletonGuidService singletonGuidService { get; set; }

        [Inject]
        private IScopedGuidService scopedGuidService { get; set; }

        [Inject]
        private ITransientGuidService transientGuidService { get; set; }

        private Guid singletonGuidId { get; set; }

        private Guid scopedGuidId { get; set; }

        private Guid transientGuidId { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
            createSingletonGuid();
            createScopedGuid();
            createTransientGuid();
        }

        private void createSingletonGuid()
        {
            singletonGuidId = singletonGuidService.GuidId;
            StateHasChanged();
        }

        private void createScopedGuid()
        {
            scopedGuidId = scopedGuidService.GuidId;
            StateHasChanged();
        }

        private void createTransientGuid()
        {
            transientGuidId = transientGuidService.GuidId;
            StateHasChanged();
        }

    }
}
