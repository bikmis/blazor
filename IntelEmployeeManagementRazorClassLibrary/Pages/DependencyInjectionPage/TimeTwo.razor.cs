﻿using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Time_Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.DependencyInjectionPage
{
    public partial class TimeTwo
    {
        [Inject]
        private ISingletonTimeService singletonTimeService { get; set; }

        [Inject]
        private IScopedTimeService scopedTimeService { get; set; }

        [Inject]
        private ITransientTimeService transientTimeService { get; set; }

        private TimeSpan singletonTime { get; set; }

        private TimeSpan scopedTime { get; set; }

        private TimeSpan transientTime { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((IAuthenticationService)authenticationService).GuardRoute();
            createSingletonTime();
            createScopedTime();
            createTransientTime();
        }

        private void createSingletonTime()
        {
            singletonTime = singletonTimeService.Time;
            StateHasChanged();
        }

        private void createScopedTime()
        {
            scopedTime = scopedTimeService.Time;
            StateHasChanged();
        }

        private void createTransientTime()
        {
            transientTime = transientTimeService.Time;
            StateHasChanged();
        }
    }
}
