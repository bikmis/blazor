using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Guid_Service
{
    public class ScopedGuidService : IScopedGuidService
    {
        public Guid GuidId { get; set; }

        public ScopedGuidService()
        {
            GuidId = Guid.NewGuid();
        }
    }
}
