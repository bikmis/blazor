using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, object data);
    }
}
