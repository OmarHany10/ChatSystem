using ChatSystem.Core.DTOs;
using ChatSystem.Core.Helpers;
using ChatSystem.Core.Models;
using ChatSystem.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace ChatSystem.Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JWT jWT;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jWT)
        {
            this.userManager = userManager;
            this.jWT = jWT.Value;
        }

        public async Task<byte[]> GetPicture(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user.Picture;
        }

        public async Task<UserDTO> GetUser(string userId)
        {
            var user =  await userManager.FindByIdAsync(userId);

            var userDTO = new UserDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Password = "**********",
            };

            return userDTO;

        }

        public async Task<bool> LoadPicture(IFormFile picture, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            using (var ms = new MemoryStream())
            {
                picture.CopyTo(ms);
                var data = ms.ToArray();
                user.Picture = data;
            }
            var result = await userManager.UpdateAsync(user);
            
            return result.Succeeded ? true : false;
        }

        public async Task<TokenDTO> Login(LoginDTO loginDTO)
        {
            ApplicationUser user = await userManager.FindByNameAsync(loginDTO.Username);
            if (user == null)
                return new TokenDTO { Message = "Incorrect Username or Password" };
            bool result = await userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!result)
                return new TokenDTO { Message = "Incorrect Username or Password" };

            //Generate Token
            var jwtSecurityToken = await CreateToken(user);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var TokenDTO = new TokenDTO { Token = token, ExpireDate = jwtSecurityToken.ValidTo };
            return TokenDTO;
        }

        public async Task<TokenDTO> Register(UserDTO userDTO)
        {
            if (await userManager.FindByNameAsync(userDTO.Username) is not null)
                return new TokenDTO { Message = "This Username already token" };
            if (await userManager.FindByEmailAsync(userDTO.Email) is not null)
                return new TokenDTO { Message = "This Email already token" };
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userDTO.Username,
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
            };

            IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new TokenDTO { Message = errors };
            }

            // Generate Token

            var jwtSecurityToken = await CreateToken(user);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var tokenDTO = new TokenDTO { Token = token, ExpireDate = jwtSecurityToken.ValidTo };
            return tokenDTO;
        }

        private async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var UserRoles = await userManager.GetRolesAsync(user);
            var UserRoleClaims = new List<Claim>();
            foreach (var userRole in UserRoles)
            {
                UserRoleClaims.Add(new Claim("role", userRole));
            }

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id),
                new Claim("uname", user.UserName),
            }.Union(userClaims).Union(UserRoleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWT.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jWT.Issuer,
                audience: jWT.Audience,
                claims: Claims,
                expires: DateTime.Now.AddDays(jWT.Duration),
                signingCredentials: signingCredentials
                );

            return token;
        }
    }
}
