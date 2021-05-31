using Microsoft.JSInterop;
using RazorClassLibrary31.Helper;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.AuthenticationService;
using RazorClassLibrary31.Services.SerializerService;
using RazorClassLibrary31.Services.TokenService;
using RazorClassLibrary31.Services.UserService;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.HttpService
{
    public class HttpService : IHttpService
    {
        private ISerializerService serializerService;
        private ITokenService tokenService;
        private IJSRuntime jsRuntime { get; set; }
        private IAuthenticationService authenticationService { get; set; }

        public HttpService(ISerializerService _serializerService, ITokenService _tokenService, IAuthenticationService _authenticationService, IJSRuntime _jsRuntime)
        {
            serializerService = _serializerService;
            tokenService = _tokenService;
            authenticationService = _authenticationService;
            jsRuntime = _jsRuntime;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, object data)
        {
            if (Utility.IsTokenExpired(tokenService.AccessToken))
            {
                var refreshToken = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "refresh_token");
                if (refreshToken == null || Utility.IsTokenExpired(refreshToken))
                {
                    authenticationService.LogoutUser();
                    var responseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Refresh token has expired or is not available." };
                    return responseMessage;
                }
                var tokenResponse = await sendAsync(new HttpClient() { BaseAddress = new Uri("https://localhost:44382/") }, HttpMethod.Post, "api/refreshToken", null, refreshToken);
                var token = await serializerService.DeserializeToType<Token>(tokenResponse);
                tokenService.AccessToken = token.AccessToken;
            }

            var response = await sendAsync(httpClient, method, url, data, tokenService.AccessToken);
            return response;
        }

        private async Task<HttpResponseMessage> sendAsync(HttpClient httpClient, HttpMethod method, string url, object data, string token)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = serializerService.SerializeToString(data); //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            var response = await httpClient.SendAsync(request);
            return response;
        }

    }
}
