using JwtServer31.Models.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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

            var jwt = createJwt(request.Email);
            var response = new LoginResponse() { Jwt = jwt };
            return Ok(response);
        }

        private string createJwt(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your Security Key Goes Here."));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(issuer: "domain.com", audience: "domain.com", claims: claims, notBefore: null, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
