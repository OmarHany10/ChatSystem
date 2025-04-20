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

            AddRefreshTokenToCookie(result.RefreshToken, result.RefreshTokenExpiresOn);
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

            if (!string.IsNullOrEmpty(result.RefreshToken))
                AddRefreshTokenToCookie(result.RefreshToken, result.RefreshTokenExpiresOn);

            return Ok(result);
        }

        [HttpGet(ApiRoutes.RefreshRoute)]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest();

            var result = await authService.RefreshToken(refreshToken);

            if (result.Message != null)
                return BadRequest(result.Message);

            AddRefreshTokenToCookie(result.RefreshToken, result.RefreshTokenExpiresOn);

            return Ok(result);

        }

        [HttpGet(ApiRoutes.RevokeRoute)]
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest();

            var result = await authService.RevokeToken(refreshToken);

            if (!result)
                return BadRequest("Invalid Token");

            return Ok();

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


        private void AddRefreshTokenToCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires,
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
