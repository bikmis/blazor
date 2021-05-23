using System;
using System.Collections.Generic;
using System.Text;

namespace RazorClassLibrary31.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private string accessToken;
        private string refreshToken;

        public string AccessToken { get => accessToken; set => accessToken = value; }
        public string RefreshToken { get => refreshToken; set => refreshToken = value; }
    }
}
