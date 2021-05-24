using System;
using System.Collections.Generic;
using System.Text;

namespace RazorClassLibrary31.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private string accessToken;
        private string refreshToken;
        private bool isLoggedIn;

        public string AccessToken { get => accessToken; set => accessToken = value; }
        public string RefreshToken { get => refreshToken; set => refreshToken = value; }
        public bool IsLoggedIn { get => isLoggedIn; set => isLoggedIn = value; }  //IsLoggedIn is here because TokenService is a singleton in contrast to LoginService which is scoped (AddHttpClient)
    }
}
