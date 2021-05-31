using System;
using System.Collections.Generic;
using System.Text;

namespace RazorClassLibrary31.Services.Guid_Service
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
