﻿using JwtServer31.Models.Login;
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

        [Authorize]
        [Route("refreshToken")]
        [HttpPost]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Headers["Authorization"].ToString().Split(" ")[1]; 
            var handler = new JwtSecurityTokenHandler();
            var readableRefreshtoken = handler.ReadJwtToken(refreshToken);
            var email = extractClaim(readableRefreshtoken, "email");
           // var securityKey = "Your Refresh Token Security Key Goes Here.";
           // var issuer = extractClaim(readableRefreshtoken, "iss");
           // var audience = extractClaim(readableRefreshtoken, "aud");

            var accessToken = createJwt(email, "Your Security Key Goes Here.", "https://localhost:44382/", "https://localhost:44327/", 1);
           // var newRefreshToken = createJwt(email, securityKey, issuer, audience, 60);

            //sending back the same old refresh token as it has not expired as yet. When refresh token expires, the user needs to login again to get new access and refresh tokens.
            var response = new RefreshTokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return Ok(response);
        }

        private string extractClaim(JwtSecurityToken token, string claimType) 
        {
            var claimValue = token.Claims.ToList().Where(claim => claim.Type == claimType).FirstOrDefault().Value;
            return claimValue;
        }
    }

}
