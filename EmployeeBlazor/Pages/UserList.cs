using EmployeeBlazor.Models;
using EmployeeBlazor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBlazor.Pages
{
    public partial class UserList
    {
        [Inject]
        public IUserService UserService { get; set; }
        public List<User> Users { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Users = (await UserService.GetUsers()).ToList();
        }
    }
}
