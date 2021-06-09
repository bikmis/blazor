using Intel.EmployeeManagement.IdentityProvider.Models.Login;
using Intel.EmployeeManagement.IdentityProvider.Models.AccessToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Intel.EmployeeManagement.IdentityProvider.Services.Database_Service;
using Intel.EmployeeManagement.Data.Entities;
using System.Collections.Generic;

namespace Intel.EmployeeManagement.IdentityProvider.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IDatabaseService databaseService { get; set; }
        private string accessTokenSecurityKey = "Your Security Key Goes Here.";
        private string refreshTokenSecurityKey = "Your Refresh Token Security Key Goes Here.";
        public LoginController(IDatabaseService _databaseService)
        {
            databaseService = _databaseService;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return Unauthorized();
            }

            var user = databaseService.EmployeeDbContext.Users.Where(u => u.Email == request.Email && u.Password == request.Password).FirstOrDefault();
            if (user == null)
            {
                return Unauthorized();
            }

            var roleNames = databaseService.EmployeeDbContext.Roles.Where(r => r.UserID == user.ID).Select(role => role.RoleName).ToList();

            var accessToken = createJwt(user, roleNames, accessTokenSecurityKey, issuer: "https://localhost:44382/", audience: "https://localhost:44327/", expiryInMinutes: 1440);        //https://localhost:44327/ is the base address of resource (employee) server
            var refreshToken = createJwt(user, roleNames, refreshTokenSecurityKey, issuer: "https://localhost:44382/", audience: "https://localhost:44382/", expiryInMinutes: 2880);      //https://localhost:44382/ is the base address of token server
            var response = new LoginResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return Ok(response);
        }

        private string createJwt(User user, List<string> roleNames, string securityKey, string issuer, string audience, double expiryInMinutes)
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

        //This endpoint needs a valid refresh token. If refresh token expires, user needs to login to get access and refresh token.
        //This endpoint is used only when access token has expired or when the page is refreshed and a local variable in a service loses the access token.
        [Authorize]
        [Route("accessToken")]
        [HttpPost]
        public IActionResult AccessToken()
        {            
            var refreshToken = Request.Headers["Authorization"].ToString().Split(" ")[1];
            User user = new User()
            {
                ID = int.Parse(readToken(refreshToken, "id").FirstOrDefault()),
                Username = readToken(refreshToken, "username").FirstOrDefault(),
                Email = readToken(refreshToken, "email").FirstOrDefault()
            };
            var roles = readToken(refreshToken, "role");
            var issuer = readToken(refreshToken, "iss").FirstOrDefault();

            var accessToken = createJwt(user, roles, accessTokenSecurityKey, issuer, audience: "https://localhost:44327/", expiryInMinutes: 1440);

            //Sending back a new access token and but not the old refresh token which has not expired as yet.
            var response = new AccessTokenResponse()
            {
                AccessToken = accessToken,
            };
            return Ok(response);
        }

        private List<string> readToken(string token, string claimType) {
            var handler = new JwtSecurityTokenHandler();
            var readableRefreshtoken = handler.ReadJwtToken(token);
            var claimValues = readableRefreshtoken.Claims.ToList().Where(claim => claim.Type == claimType).Select(claim => claim.Value);
            return claimValues.ToList();
        }

    }

}
