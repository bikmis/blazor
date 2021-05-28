using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace RazorClassLibrary31.Helper
{
    public static class Utility
    {
        public static string ReadToken(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token);
            var claimValue = jsonToken.Claims.ToList().Where(claim => claim.Type == claimType).FirstOrDefault()?.Value;
            return claimValue;
        }

    }
}
