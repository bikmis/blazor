using Intel.EmployeeManagement.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Intel.EmployeeManagement.EmployeeWebApi.IntegrationTests_1
{
    public class SharedService
    {
        public static string CreateJwt(User user, List<string> roleNames, string securityKey, string issuer, string audience, double expiryInMinutes)
        {
            var symmetircSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(symmetircSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim("id", user.ID.ToString()),
                new Claim("username", user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roleNames.Select(roleName => new Claim("role", roleName)));

            //https://docs.microsoft.com/en-us/linkedin/shared/authentication/programmatic-refresh-tokens#:~:text=By%20default%2C%20access%20tokens%20are,application%20when%20refresh%20tokens%20expire.
            //Use Utc time. Even if you use local time such as Eastern, the token expiration will convert to Utc.
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, notBefore: null, expires: DateTime.UtcNow.AddMinutes(expiryInMinutes), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static HttpContent Serialize(object data)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            return stringContent;
        }

        public static T DeserializeToType<T>(HttpResponseMessage response)
        {
            var deserializedToType = JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return deserializedToType;
        }
    }
}
