using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Logging
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {     
        private IHttpService httpService { get; set; }

        public ApplicationLoggerProvider(IHttpService _httpService)
        {
            httpService = _httpService;
        }    

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(httpService);
        }

        public void Dispose()
        {
            
        }
    }
}
