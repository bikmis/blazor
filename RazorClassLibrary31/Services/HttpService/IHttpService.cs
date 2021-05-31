using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.Http_Service
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod method, string url, object data);
    }
}
