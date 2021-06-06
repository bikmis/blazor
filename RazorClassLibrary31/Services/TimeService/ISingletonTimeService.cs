using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Time_Service
{
    public interface ISingletonTimeService
    {
        TimeSpan Time { get; set; } 
    }
}
