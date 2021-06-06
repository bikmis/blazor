using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Time_Service
{
    public class ScopedTimeService : IScopedTimeService
    {
        public TimeSpan Time { get; set; }

        public ScopedTimeService()
        {
            Time = DateTime.Now.TimeOfDay;
        }
    }
}
