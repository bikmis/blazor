using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace RazorClassLibrary31.Helper
{
    public class Utility
    {
        public static string ReadToken(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token);
            var claimValue = jsonToken.Claims.ToList().Where(claim => claim.Type == claimType).FirstOrDefault()?.Value;
            return claimValue;
        }

        public static bool IsTokenExpired(string token)
        {
            var expiryInSeconds = long.Parse(ReadToken(token, "exp"));  //exp is in NumericDate - number of seconds from 1970-01-01T00:00:00Z UTC until the specified UTC date/time, ignoring leap seconds.
            var utcTimeNowInSecondsFrom1970 = new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero).ToUnixTimeSeconds();
            var isExpired = utcTimeNowInSecondsFrom1970 > (expiryInSeconds - 10); //In the UI, token expires 10 seconds earlier, so that we don't have to use a token that is expiring soon and have it rejected by the server as it takes a while to reach the server.
            return isExpired;
        }

    }
}
