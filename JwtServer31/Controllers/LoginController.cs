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

namespace Intel.EmployeeManagement.IdentityProvider.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IDatabaseService databaseService { get; set; }
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

            var securityKey = "Your Security Key Goes Here.";
            var accessToken = createJwt(user, securityKey, issuer: "https://localhost:44382/", audience: "https://localhost:44327/", expiryInMinutes: 1440);        //https://localhost:44327/ is the base address of resource (employee) server
            var refreshToken = createJwt(user, "Your Refresh Token Security Key Goes Here.", issuer: "https://localhost:44382/", audience: "https://localhost:44382/", expiryInMinutes: 2880);      //https://localhost:44382/ is the base address of token server
            var response = new LoginResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return Ok(response);
        }

        private string createJwt(User user, string securityKey, string issuer, string audience, double expiryInMinutes)
        {
            var symmetircSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(symmetircSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("id", user.ID.ToString()),
                new Claim("username", user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", "admin"),
                new Claim("role", "employee"),
            };

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
                ID = int.Parse(readToken(refreshToken, "id")),
                Username = readToken(refreshToken, "username"),
                Email = readToken(refreshToken, "email")
            };

            var issuer = readToken(refreshToken, "iss");
            var accessToken = createJwt(user, "Your Security Key Goes Here.", issuer, audience: "https://localhost:44327/", expiryInMinutes: 1440);

            //Sending back a new access token and but not the old refresh token which has not expired as yet.
            var response = new AccessTokenResponse()
            {
                AccessToken = accessToken,
            };
            return Ok(response);
        }

        private string readToken(string token, string claimType) {
            var handler = new JwtSecurityTokenHandler();
            var readableRefreshtoken = handler.ReadJwtToken(token);
            var claimValue = readableRefreshtoken.Claims.ToList().Where(claim => claim.Type == claimType).FirstOrDefault().Value;
            return claimValue;
        }

    }

}
