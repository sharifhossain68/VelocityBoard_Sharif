using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityBoard.Core.DTOs;
using VelocityBoard.Core.Entities;

namespace VelocityBoard.Core.Interface
{
    public interface IJWTRepository
    {
        Task<User> Register(User user);
        Task<User?> GetLoginData(LoginDTO loginDTO);
    }

}
