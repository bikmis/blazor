using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.App_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service
{
    public class HttpService : IHttpService
    {
        private IAppService appService;
        private AuthenticationStateProvider authenticationService { get; set; }
        private HttpClient httpClient { get; set; }

        public HttpService(IAppService _appService, AuthenticationStateProvider _authenticationService, HttpClient _httpClient)
        {
            appService = _appService;
            authenticationService = _authenticationService;
            httpClient = _httpClient;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, object data)
        {
            try
            {
                var response = await sendAsync(method, url, data);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)  //if access token is expired, response is unauthorized.
                {
                    response = await ((AuthenticationService)authenticationService).GetAccessToken();  //get access token using refresh token
                    if (response.IsSuccessStatusCode) //if refresh token is available and not expired
                    {
                        var token = await appService.Deserialize<Token>(response);
                        appService.AccessToken = token.AccessToken;
                        response = await sendAsync(method, url, data);
                        return response;
                    }
                    else
                    {
                        //if access token is expired and refresh token is expired/unavailable(deleted from session storage), then user is logged out.
                        ((AuthenticationService)authenticationService).LogoutUser();
                        return response;
                    }
                }
                else
                {
                    //if api server returns a status code other than success codes (200 to 299) and unauthorized(401) e.g. 500 internal server error, the execution comes here.
                    return response;
                }
            }
            catch (Exception)
            {
                //if API/resource server is down, exception is caught here and rethrown. For Blazor Web Assembly if database is down, exception is thrown and code executes here.
                throw;
            }
        }

        private async Task<HttpResponseMessage> sendAsync(HttpMethod method, string url, object data)
        {
            var request = new HttpRequestMessage(method, url);
            if (appService.AccessToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", appService.AccessToken);
            }
            request.Content = appService.Serialize(data); //enable cors (AllowAnyOrigin & AllowAnyHeader) in web api project to accept any request URL & Content-Type "application/json"
            var response = await httpClient.SendAsync(request);
            return response;
        }
    }
}
