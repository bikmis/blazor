using EmployeeBlazor.Models;
using EmployeeBlazor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBlazor.Pages.UserPage
{
    //This is a code-behind file for UserList.razor. It is a partial class with the same name as the razor.
    public partial class UserList
    {
        [Inject]
        public IUserService UserService { get; set; }
        public List<User> Users { get; set; } = new List<User>(); //Assign an empty object or check null in the razor to avoid an exception.

        protected async override Task OnInitializedAsync()
        {
            Users = (await UserService.GetUsers()).ToList();
        }
    }
}
