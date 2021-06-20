using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.WebAPI.Models.Logging
{
    public class LogRequest
    {
        public string LogLevel { get; set; }
        public string EventName { get; set; }
        public string Source { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
