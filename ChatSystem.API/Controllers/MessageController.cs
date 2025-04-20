using ChatSystem.API.Hubs;
using ChatSystem.Core.Constants;
using ChatSystem.Core.DTOs;
using ChatSystem.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly IGroupService groupService;
        private readonly IConnectionService connectionService;
        private readonly IHubContext<ChatHub> hubContext;

        public MessageController(IMessageService messageService, IGroupService groupService, IConnectionService connectionService,IHubContext<ChatHub> hubContext)
        {
            this.messageService = messageService;
            this.groupService = groupService;
            this.connectionService = connectionService;
            this.hubContext = hubContext;
        }

        [HttpGet("{groupId:int}")]
        public async Task<IActionResult> GetAllMessageInGroup(int groupId)
        {
            var result = await messageService.GetMessagesInGroup(groupId);
            return Ok(result);
        }

        [HttpGet("{friendUsername}")]
        public async Task<IActionResult> GetAllMessageInPrivateChat(string friendUsername)
        {
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var result = await messageService.GetMessagesInPrivateChat(friendUsername ,userId);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.GroupRoute)]
        public async Task<IActionResult> SendToGroup(GroupMessageDTO groupMessageDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var result = messageService.SendMessageToGroupConversation(groupMessageDTO, userId);
            if(result != null)
                return BadRequest(result);

            #region SignalR
            var group = groupService.GetGroup(groupMessageDTO.GroupId);
            var username = User.FindFirst(ClaimNames.Username)?.Value;
            await hubContext.Clients.Groups(group.Name).SendAsync(ResponseMessages.NewGroupMessageMethod, username, groupMessageDTO.Text); 
            #endregion

            return Ok(ResponseMessages.SuccessMessage);
        }

        [HttpPost(ApiRoutes.PrivateRoute)]
        public async Task<IActionResult> SendToPrivate(PrivateMessageDTO privateMessageDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var result = await messageService.SendMessageToPrivateConversation(privateMessageDTO, userId);
            if (result != null)
                return BadRequest(result);

            #region SignalR
            var reciverConnectionsId = await connectionService.GetConnectionsId(privateMessageDTO.FriendUsername);
            var username = User.FindFirst(ClaimNames.Username)?.Value;
            var MyconnectionsId = await connectionService.GetConnectionsId(username);
            var allConnectionsId = reciverConnectionsId.Concat(MyconnectionsId).Distinct();
            foreach (var connectionId in allConnectionsId)
            {
                await hubContext.Clients.Client(connectionId).SendAsync(ResponseMessages.NewPrivateMessageMethod, username, privateMessageDTO.Text);
            } 
            #endregion

            return Ok(ResponseMessages.SuccessMessage);
        }


    }
}
