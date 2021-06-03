using Intel.EmployeeManagement.RazorClassLibrary.Models;


namespace Intel.EmployeeManagement.RazorClassLibrary.Services.User_Service
{
    public class UserService : IUserService
    {
        private User _user = new User();

        public User User { get => _user; set => _user = value; }
    }
}