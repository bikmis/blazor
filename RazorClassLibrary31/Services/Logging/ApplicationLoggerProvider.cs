using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Logging
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {
        
        private HttpClient httpClient { get; set; }

        public ApplicationLoggerProvider(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(httpClient);
        }

        public void Dispose()
        {
            
        }
    }
}
