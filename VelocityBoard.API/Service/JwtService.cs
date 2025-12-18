using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VelocityBoard.Core.DTOs;
using VelocityBoard.Core.Entities;
using VelocityBoard.Infrastructure.Repostories;

namespace VelocityBoard.API.Service
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        private readonly JWTRepository _jwtDataService;


        public JwtService(IConfiguration config, JWTRepository jWTRepository)
        {
            _config = config;
            _jwtDataService = jWTRepository;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User> Register(User user)
        {
            return await _jwtDataService.Register(user);
        }
        public async Task<User?> GetLoginData(LoginDTO  loginDTO)
        {
            return await _jwtDataService.GetLoginData(loginDTO);
        }
    }
}
