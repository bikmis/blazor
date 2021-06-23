using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Intel.EmployeeManagement.BlazorClient.Tests.Services
{
    public class MockHttpClient
    {
        public HttpClient HttpClient;
        public MockHttpClient()
        {
            HttpClient = new HttpClient();
        }
    }
}
