using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.App_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service
{
    public class HttpService : IHttpService
    {
        private IAppService appService;
        private IJSRuntime jsRuntime { get; set; }
        private AuthenticationStateProvider authenticationService { get; set; }

        public HttpService(IAppService _appService, AuthenticationStateProvider _authenticationService, IJSRuntime _jsRuntime)
        {
            appService = _appService;
            authenticationService = _authenticationService;
            jsRuntime = _jsRuntime;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, object data)
        {
            var response = await ((AuthenticationService)authenticationService).SendAsync(httpClient, method, url, data, appService.AccessToken);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)  //if access token is expired, response is unauthorized.
            {
                var refreshToken = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "refresh_token");
                response = await ((AuthenticationService)authenticationService).SendAsync(new HttpClient() { BaseAddress = new Uri("https://localhost:44382/") }, HttpMethod.Post, "api/accessToken", null, refreshToken);
                if (response.IsSuccessStatusCode) //if refresh token is expired, resonse is unauthorized
                {
                    var token = await appService.DeserializeToType<Token>(response);
                    appService.AccessToken = token.AccessToken;
                    response = await ((AuthenticationService)authenticationService).SendAsync(httpClient, method, url, data, appService.AccessToken);
                    return response;
                }
            }

            //if access token is expired and refresh token is expired/unavailable(deleted from session storage), then user is logged out.
            ((AuthenticationService)authenticationService).LogoutUser();
            return response;
        }
    }
}
