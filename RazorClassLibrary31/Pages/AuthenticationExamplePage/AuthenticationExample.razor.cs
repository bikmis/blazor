﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Services.Authentication_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.AuthenticationExamplePage
{
    partial class AuthenticationExample
    {   

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }
      

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();
        }

    }
}
