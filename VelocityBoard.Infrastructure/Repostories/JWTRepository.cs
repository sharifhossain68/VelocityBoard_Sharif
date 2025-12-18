using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityBoard.Core.DTOs;
using VelocityBoard.Core.Entities;
using VelocityBoard.Core.Interface;
using VelocityBoard.Infrastructure.Data;

namespace VelocityBoard.Infrastructure.Repostories
{
    public class JWTRepository : IJWTRepository
    {
        private readonly VelocityBoardDbContext _context;
        public async Task<User> Register(User  user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                // Roll back transaction if something failed

                throw new Exception($"Error creating register: {ex.Message}");
            }
    }
        public async Task<User?> GetLoginData(LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);
            return   user;
        }
    }
}
