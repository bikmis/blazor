using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Time_Service
{
    public class SingletonTimeService : ISingletonTimeService
    {
        public TimeSpan Time { get; set; }

        public SingletonTimeService()
        {
            Time = DateTime.Now.TimeOfDay;
        }
    }
}
