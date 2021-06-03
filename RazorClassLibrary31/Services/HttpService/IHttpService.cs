﻿using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.Personnel.RazorClassLibrary.Services.Http_Service
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, object data);
    }
}
