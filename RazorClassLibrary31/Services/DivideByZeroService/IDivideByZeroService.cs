using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.DivideByZeroService
{
    public interface IDivideByZeroService
    {
        Task<HttpResponseMessage> DivideByZero();
    }
}
