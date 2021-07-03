using System;
using System.Collections.Generic;
using System.Text;

namespace Intel.EmployeeManagement.Data.Entities
{
    public class Log
    {
        public int ID { get; set; }
        public string LogLevel { get; set; }
        public string EventName { get; set; }
        public string Source { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
