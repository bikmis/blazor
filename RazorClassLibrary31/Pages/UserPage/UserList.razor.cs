using Microsoft.AspNetCore.Components;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.UserService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.UserPage
{
    //This is a code-behind file for UserList.razor. It is a partial class with the same name as the razor.
    public partial class UserList : ComponentBase
    {
        [Inject]
        private IUserService userService { get; set; }
        private List<User> users { get; set; } = new List<User>(); //Assign an empty object or check null in the razor to avoid an exception.

        protected async override Task OnInitializedAsync()
        {
            users = (await userService.GetUsers()).ToList();
        }
    }
}
