using Microsoft.AspNetCore.Components;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.User_Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Shared
{
    public partial class MainLayout
    {
        private string emailAddress { get; set; }

        [Inject]
        private IUserService userService { get; set; }

        protected override Task OnInitializedAsync()
        {
            emailAddress = userService.User.Email;
            return base.OnInitializedAsync();
        }
    }
}
