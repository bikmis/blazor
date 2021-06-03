using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Guid_Service
{
    public interface IGuidService
    {
        Guid CreateGuid();
        int Increment();
    }
}
