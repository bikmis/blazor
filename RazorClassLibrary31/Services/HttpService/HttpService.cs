using RazorClassLibrary31.Services.TokenService;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, StringContent data)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenService.Jwt);
            request.Content = data;
            var response = await httpClient.SendAsync(request);
            return response;
        }
    }
}
