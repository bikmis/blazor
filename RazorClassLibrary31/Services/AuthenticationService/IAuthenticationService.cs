using RazorClassLibrary31.Models;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> LoginUser(Login login);
        void LogoutUser(User user);
    }
}
