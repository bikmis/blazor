using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.LoggingHowToPage
{
    public partial class LoggingHowTo
    {
        [Inject]
        private ILogger<LoggingHowTo> logger { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private void log() {
            logger.LogTrace("logging trace");
            logger.LogDebug("logging debug");
            logger.LogInformation("logging information");
            // logger.LogError("logging error");
            logger.LogWarning("logging warning");
            // logger.LogCritical("logging critical");
            Console.WriteLine("Logging into browser console."); //only for client side(web assembly), not for server side
        }
    }
}
