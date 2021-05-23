using RazorClassLibrary31.Services.SerializerService;
using RazorClassLibrary31.Services.TokenService;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.HttpService
{
    public class HttpService : IHttpService
    {
        private ITokenService tokenService;
        private ISerializerService serializerService;

        public HttpService(ITokenService _tokenService, ISerializerService _serializerService)
        {
            tokenService = _tokenService;
            serializerService = _serializerService;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, object data)
        {
            var request = new HttpRequestMessage(method, url);
            if (tokenService.AccessToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenService.AccessToken);
            }
            request.Content = serializerService.SerializeToString(data); //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            var response = await httpClient.SendAsync(request);
            return response;
        }
    }
}
