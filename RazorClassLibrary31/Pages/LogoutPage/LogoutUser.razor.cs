using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary31.Services.UserService;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.LogoutPage
{
    partial class LogoutUser
    {
        [Inject]
        private IUserService userService { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        private void logout() {
            userService.User.IsLoggedIn = false;
            navigationManager.NavigateTo("/");
        }

    }
}
