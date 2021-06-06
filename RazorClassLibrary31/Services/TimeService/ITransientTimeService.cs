using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Time_Service
{
    public interface ITransientTimeService
    {
        TimeSpan Time { get; set; }
    }
}
 