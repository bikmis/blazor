using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.ClientVsServerPage
{
    public partial class ClientVsServer
    {
        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
        }
    }
}
