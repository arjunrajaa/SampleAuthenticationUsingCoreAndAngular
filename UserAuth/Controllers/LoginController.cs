using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Accessors.Concrete;
using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserAuth.Models;

namespace UserAuth.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserAccessor _userAccessor;

        public LoginController(IConfiguration config, IUserAccessor userAccessor)
        {
            _config = config;
            _userAccessor = userAccessor;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UserAuthDto login)
        {
            IActionResult response = Unauthorized();
            UserDto user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWT(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }

            return response;
        }

        UserDto AuthenticateUser(UserAuthDto loginCredentials)
        {

            var user = _userAccessor.GetUserFromUNameAndPassword(loginCredentials.UserName, loginCredentials.Password);
            return user;
        }

        string GenerateJWT(UserDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("firstName", userInfo.FirstName.ToString()),
                new Claim("role",userInfo.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
