﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RazorClassLibrary31.Services.TokenService
{
    public interface ITokenService
    {
        string Jwt { get; set; }
    }
}
