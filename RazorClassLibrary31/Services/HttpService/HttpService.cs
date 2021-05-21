using RazorClassLibrary31.Services.TokenService;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.HttpService
{
    public class HttpService : IHttpService
    {
        private ITokenService tokenService;

        public HttpService(ITokenService _tokenService)
        {
            tokenService = _tokenService;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, object data)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenService.Jwt);
            var content = data != null ? new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json") : null;   //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            request.Content = content;
            var response = await httpClient.SendAsync(request);
            return response;
        }
    }
}
