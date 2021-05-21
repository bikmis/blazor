using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.HttpService
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, StringContent data = null);
    }
}
