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
        public Task<TokenDTO> Register(UserDTO userDTO);
        public Task<TokenDTO> Login(LoginDTO loginDTO);
        public Task<UserDTO> GetUser(string userId);
        public Task<bool> LoadPicture(IFormFile picture, string userId);
        public Task<byte []> GetPicture(string userId);
        
    }
}
