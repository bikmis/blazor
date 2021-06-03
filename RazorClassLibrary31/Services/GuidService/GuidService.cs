using System;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Guid_Service
{
    public class GuidService : IGuidServiceAddScoped, IGuidServiceAddSingleton, IGuidServiceAddTransient
    {
        private int counter { get; set; }
        private Guid guid { get; set; }
        public GuidService()
        {
            guid = Guid.NewGuid();
            counter++;
        }

        public Guid CreateGuid() {
            return guid;
        }

        public int Increment() {           
            return counter;
        }
    }
}
