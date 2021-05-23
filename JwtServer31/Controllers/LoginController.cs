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
            var accessToken = createJwt(request.Email, securityKey, "domain.com", "domain.com");
            var refreshToken = createJwt(request.Email, "Your Refresh Token Security Key Goes Here.", "TokenServer.com", "TokenServer.com");
            var response = new LoginResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return Ok(response);
        }

        private string createJwt(string email, string securityKey, string issuer, string audience)
        {
            var symmetircSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(symmetircSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, notBefore: null, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        [Route("refreshToken")]
        [HttpPost]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Headers["Authorization"].ToString().Split(" ")[1]; 
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(refreshToken);
            var email = token.Claims.ToList().Where(claim => claim.Type == "email").FirstOrDefault().Value;
            var securityKey = "Your Refresh Token Security Key Goes Here.";
            var issuer = token.Claims.ToList().Where(claim => claim.Type == "iss").FirstOrDefault().Value;
            var audience = token.Claims.ToList().Where(claim => claim.Type == "aud").FirstOrDefault().Value;

            var accessToken = createJwt(email, "Your Security Key Goes Here.", "domain.com", "domain.com");
            var newRefreshToken = createJwt(email, securityKey, issuer, audience);

            var response = new RefreshTokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
            return Ok(response);
        }
    }

}
