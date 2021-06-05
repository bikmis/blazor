using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Guid_Service
{
    public class SingletonGuidService : ISingletonGuidService
    {
        public Guid GuidId { get; set; }

        public SingletonGuidService()
        {
            GuidId = Guid.NewGuid();
        }
    }
}
