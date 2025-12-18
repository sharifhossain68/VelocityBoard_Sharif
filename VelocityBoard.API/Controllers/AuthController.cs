using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VelocityBoard.API.Service;
using VelocityBoard.Core.DTOs;
using VelocityBoard.Core.Entities;
using VelocityBoard.Infrastructure.Data;

namespace VelocityBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _service;
        private readonly JwtService _jwt;

        public AuthController( JwtService jwt)
        {
          
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO  registerDTO)
        {
            var user = new User
            {
                FullName = registerDTO.FullName,
                Email = registerDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            };

             await _service.Register(user);
            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO  loginDTO)
        {
            var user = await _service.GetLoginData(loginDTO);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
                return Unauthorized();

            var token = _jwt.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
