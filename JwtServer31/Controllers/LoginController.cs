using JwtServer31.Models.Login;
using JwtServer31.Models.RefreshToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JwtServer31.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            //check credentials, if credentials are bad, return Unauthorized.
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return Unauthorized();
            }
            var securityKey = "Your Security Key Goes Here.";
            var accessToken = createJwt(request.Email, securityKey, issuer: "https://localhost:44382/", audience: "https://localhost:44327/", 1);  //https://localhost:44327/ is the base address of resource (employee) server
            var refreshToken = createJwt(request.Email, "Your Refresh Token Security Key Goes Here.", issuer: "https://localhost:44382/", audience: "https://localhost:44382/", 60); //https://localhost:44382/ is the base address of token server
            var response = new LoginResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return Ok(response);
        }

        private string createJwt(string email, string securityKey, string issuer, string audience, double expiryInMinutes)
        {
            var symmetircSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(symmetircSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //https://docs.microsoft.com/en-us/linkedin/shared/authentication/programmatic-refresh-tokens#:~:text=By%20default%2C%20access%20tokens%20are,application%20when%20refresh%20tokens%20expire.
            //Use Utc time. Even if you use local time such as Eastern, the token expiration will convert to Utc.
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, notBefore: null, expires: DateTime.UtcNow.AddMinutes(expiryInMinutes), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //This endpoint needs a valid refresh token. If refresh token expires, user needs to login to get access and refresh token.
        //This endpoint is used only when access token has expired or when the page is refreshed and a local variable in a service loses the access token.
        [Authorize]
        [Route("refreshToken")]
        [HttpPost]
        public IActionResult RefreshToken()
        {            
            var refreshToken = Request.Headers["Authorization"].ToString().Split(" ")[1]; 
            var handler = new JwtSecurityTokenHandler();
            var readableRefreshtoken = handler.ReadJwtToken(refreshToken);
            var email = readableRefreshtoken.Claims.ToList().Where(claim => claim.Type == "email").FirstOrDefault().Value;
            var accessToken = createJwt(email, "Your Security Key Goes Here.", "https://localhost:44382/", "https://localhost:44327/", 1);

            //Sending back a new access token and but not the old refresh token which has not expired as yet.
            var response = new RefreshTokenResponse()
            {
                AccessToken = accessToken,
            };
            return Ok(response);
        }

    }

}
