using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Logging
{
    public class Logger : ILogger
    {

        HttpClient httpClient { get; set; }

        public Logger(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Log log = new Log()
            {
                LogLevel = logLevel.ToString(),
                EventName = eventId.Name,
                ExceptionMessage = exception?.Message,
                StackTrace = exception?.StackTrace,
                Source = exception?.Source,
                CreateDate = DateTime.UtcNow
            };

            var stringContent = new StringContent(JsonSerializer.Serialize(log), Encoding.UTF8, "application/json");
            httpClient.PostAsync("api/log", stringContent);
        }
    }
}

