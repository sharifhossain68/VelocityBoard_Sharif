using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VelocityBoard.API.Service;
using VelocityBoard.Core.DTOs;
using VelocityBoard.Core.Entities;
using VelocityBoard.Core.Interface;
using VelocityBoard.Infrastructure.Data;

namespace VelocityBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _service;
        

        public AuthController(JwtService jwt)
        {
          
            _service = jwt;
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
        public async Task<IActionResult> Login([FromBody] LoginDTO  loginDTO)
        {
            var user = await _service.GetLoginData(loginDTO);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
                return Unauthorized();

            var token = _service.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
