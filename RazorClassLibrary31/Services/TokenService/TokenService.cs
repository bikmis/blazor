using System;
using System.Collections.Generic;
using System.Text;

namespace RazorClassLibrary31.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private string jwt;

        public string Jwt { get => jwt; set => jwt = value; }
    }
}
