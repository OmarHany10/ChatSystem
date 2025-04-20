using ChatSystem.Core.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDTO> Register(UserDTO userDTO);
        Task<TokenDTO> Login(LoginDTO loginDTO);
        Task<UserDTO> GetUser(string userId);
        Task<bool> LoadPicture(IFormFile picture, string userId);
        Task<byte []> GetPicture(string userId);
        Task<TokenDTO> RefreshToken(string token);

        Task<bool> RevokeToken(string token);

    }
}
