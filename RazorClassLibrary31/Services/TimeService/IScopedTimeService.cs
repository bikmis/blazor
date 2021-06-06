using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Time_Service
{
    public interface IScopedTimeService
    {
        TimeSpan Time { get; set; }
    }
}
