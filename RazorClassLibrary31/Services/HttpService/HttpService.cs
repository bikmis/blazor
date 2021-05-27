using RazorClassLibrary31.Services.SerializerService;
using RazorClassLibrary31.Services.UserService;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.HttpService
{
    public class HttpService : IHttpService
    {
        private IUserService userService;
        private ISerializerService serializerService;

        public HttpService(IUserService _userService, ISerializerService _serializerService)
        {
            userService = _userService;
            serializerService = _serializerService;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, object data)
        {
            var request = new HttpRequestMessage(method, url);
            if (userService.User.AccessToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userService.User.AccessToken);
            }
            request.Content = serializerService.SerializeToString(data); //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            var response = await httpClient.SendAsync(request);
            return response;
        }
    }
}
