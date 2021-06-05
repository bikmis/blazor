using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Guid_Service
{
    public interface IScopedGuidService
    {
        Guid GuidId { get; set; }
    }
}
