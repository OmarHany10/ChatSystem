using ChatSystem.Core.Constants;
using ChatSystem.Core.DTOs;
using ChatSystem.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : ControllerBase
    {
        private readonly IFriendShipService friendShipService;

        public FriendController(IFriendShipService friendShipService)
        {
            this.friendShipService = friendShipService;
        }

        [HttpGet(ApiRoutes.MyFriends)]
        public IActionResult GetAll()
        {
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var result = friendShipService.GetAllAcceptedRequests(userId);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.MyPending)]
        public IActionResult GetAllPending()
        {
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var result = friendShipService.GetAllPendingRequests(userId);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.SendRequest)]
        public async Task<IActionResult> SendRequest(FriendDTO friendDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var result = await friendShipService.SendFriendRequest(userId, friendDTO);
            if (result != null)
                return BadRequest(result);
            return Ok(ResponseMessages.RequestSent);
        }

        [HttpPost(ApiRoutes.AcceptRequest)]
        public async Task<IActionResult> AcceptRequest(FriendDTO friendDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var result = await friendShipService.AcceptFriendRequest(userId, friendDTO);
            if (result != null)
                return BadRequest(result);

            return Ok(ResponseMessages.RequestAccepted);
        }

        [HttpPost(ApiRoutes.RejectRequest)]
        public async Task<IActionResult> RejectRequest(FriendDTO friendDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var result = await friendShipService.RejectFreindRequest(userId, friendDTO);
            if (result != null)
                return BadRequest(result);

            return Ok(ResponseMessages.RequestRejected);
        }
    }
}
