using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Guid_Service
{
    public class TransientGuidService : ITransientGuidService
    {
        public Guid GuidId { get; set; }

        public TransientGuidService()
        {
            GuidId = Guid.NewGuid();
        }
    }
}
