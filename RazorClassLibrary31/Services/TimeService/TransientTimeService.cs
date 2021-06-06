using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Time_Service
{
    public class TransientTimeService : ITransientTimeService
    {
        public TimeSpan Time { get; set; }

        public TransientTimeService()
        {
            Time = DateTime.Now.TimeOfDay;
        }
    }
}
