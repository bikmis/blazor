using System;
using System.Collections.Generic;
using System.Text;

namespace Intel.Personnel.RazorClassLibrary.Services.Guid_Service
{
    public interface IGuidService
    {
        Guid CreateGuid();
        int Increment();
    }
}
