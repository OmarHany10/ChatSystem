using ChatSystem.Core.Constants;
using ChatSystem.Core.DTOs;
using ChatSystem.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost(ApiRoutes.Register)]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await authService.Register(userDTO);
            if (result.Message != null)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Login)]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await authService.Login(loginDTO);
            if (result.Message != null)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [Authorize]
        [HttpPost(ApiRoutes.MyProfilePicture)]
        public async Task<IActionResult> LoadPicture(IFormFile image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var result = await authService.LoadPicture(image, userId);

            return result ? Ok(ResponseMessages.PictureUploadSuccess) : BadRequest(ResponseMessages.PictureUploadFailed);
        }

        [Authorize]
        [HttpGet(ApiRoutes.MyProfilePicture)]
        public async Task<IActionResult> GetPicture()
        {
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var picture = await authService.GetPicture(userId);
            return File(picture, "image/jpeg");
        }

        [Authorize]
        [HttpGet(ApiRoutes.MyDetails)]
        public async Task<IActionResult> UserDetails()
        {
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var user = await authService.GetUser(userId);
            return Ok(user);
        }

        
    }
}
