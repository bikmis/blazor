﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RazorClassLibrary31.Services.Guid_Service
{
    public interface IGuidService
    {
        Guid CreateGuid();
        int Increment();
    }
}
