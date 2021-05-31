﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.Authentication_Service;
using RazorClassLibrary31.Services.Serializer_Service;
using RazorClassLibrary31.Services.Token_Service;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.Http_Service
{
    public class HttpService : IHttpService
    {
        private ISerializerService serializerService;
        private ITokenService tokenService;
        private IJSRuntime jsRuntime { get; set; }
        private AuthenticationStateProvider authenticationService { get; set; }

        public HttpService(ISerializerService _serializerService, ITokenService _tokenService, AuthenticationStateProvider _authenticationService, IJSRuntime _jsRuntime)
        {
            serializerService = _serializerService;
            tokenService = _tokenService;
            authenticationService = _authenticationService;
            jsRuntime = _jsRuntime;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, object data)
        {
            var response = await ((AuthenticationService)authenticationService).SendAsync(httpClient, method, url, data, tokenService.AccessToken);
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
                    var token = await serializerService.DeserializeToType<Token>(response);
                    tokenService.AccessToken = token.AccessToken;
                    response = await ((AuthenticationService)authenticationService).SendAsync(httpClient, method, url, data, tokenService.AccessToken);
                    return response;
                }
            }

            //if access token is expired and refresh token is expired/unavailable(deleted from session storage), then user is logged out.
            ((AuthenticationService)authenticationService).LogoutUser();
            return response;
        }
    }
}
