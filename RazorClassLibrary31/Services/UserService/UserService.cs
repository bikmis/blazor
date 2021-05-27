using RazorClassLibrary31.Models;


namespace RazorClassLibrary31.Services.UserService
{
    public class UserService : IUserService
    {
        private User _user = new User();

        public User User { get => _user ; set => _user = value; }
    }
}