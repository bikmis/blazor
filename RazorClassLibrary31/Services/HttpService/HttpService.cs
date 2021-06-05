using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.AppStore_Service;
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
        private IAppStoreService appStoreService;
        private IJSRuntime jsRuntime { get; set; }
        private AuthenticationStateProvider authenticationService { get; set; }

        public HttpService(IAppStoreService _appStoreService, AuthenticationStateProvider _authenticationService, IJSRuntime _jsRuntime)
        {
            appStoreService = _appStoreService;
            authenticationService = _authenticationService;
            jsRuntime = _jsRuntime;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, object data)
        {
            var response = await ((AuthenticationService)authenticationService).SendAsync(httpClient, method, url, data, appStoreService.AccessToken);
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
                    var token = await appStoreService.DeserializeToType<Token>(response);
                    appStoreService.AccessToken = token.AccessToken;
                    response = await ((AuthenticationService)authenticationService).SendAsync(httpClient, method, url, data, appStoreService.AccessToken);
                    return response;
                }
            }

            //if access token is expired and refresh token is expired/unavailable(deleted from session storage), then user is logged out.
            ((AuthenticationService)authenticationService).LogoutUser();
            return response;
        }
    }
}
