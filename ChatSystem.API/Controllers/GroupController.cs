using ChatSystem.API.Hubs;
using ChatSystem.Core.Constants;
using ChatSystem.Core.DTOs;
using ChatSystem.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace ChatSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IConnectionService connectionService;
        private readonly IHubContext<ChatHub> hubContext;

        public GroupController(IGroupService groupService, IConnectionService connectionService, IHubContext<ChatHub> hubContext)
        {
            this.groupService = groupService;
            this.connectionService = connectionService;
            this.hubContext = hubContext;
        }

        [HttpGet(ApiRoutes.GetAllMembers)]
        public IActionResult GetAllMemebers(int groupId)
        {
            var result = groupService.GetAllUserInGroup(groupId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupDTO groupDTO)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            var username = User.FindFirst(ClaimNames.Username)?.Value;
            var result = await groupService.CreateGroup(groupDTO, username);
            if(result != null) 
                return BadRequest(result);

            #region SignalR
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var connectionsId = await connectionService.GetConnectionsId(userId);

            foreach (var connectionId in connectionsId)
            {
                await hubContext.Groups.AddToGroupAsync(connectionId, groupDTO.Name);
            }

            await hubContext.Clients.Groups(groupDTO.Name).SendAsync("newGroupEvent", string.Format(ResponseMessages.CreateGroupEvent, username)); 
            #endregion

            return Ok(string.Format(ResponseMessages.GroupCreated, groupDTO.Name));
        }

        [HttpPost(ApiRoutes.AddMember)]
        public async Task<IActionResult> AddToGroup(GroupMemberDTO groupMemberDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await groupService.AddToGroup(groupMemberDTO);
            if(result != null)
                return BadRequest(result);

            #region SignalR
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var connectionsId = await connectionService.GetConnectionsId(userId);
            var group = groupService.GetGroup(groupMemberDTO.GroupId);
            var username = User.FindFirst(ClaimNames.Username)?.Value;
            foreach (var connectionId in connectionsId)
            {
                await hubContext.Groups.AddToGroupAsync(connectionId, group.Name);
            }

            await hubContext.Clients.Groups(group.Name).SendAsync("newGroupEvent", string.Format(ResponseMessages.AddToGroupEvent, username, groupMemberDTO.Username));

            #endregion
            return Ok(string.Format(ResponseMessages.MemberAdded, groupMemberDTO.Username, groupMemberDTO.GroupId));
        }

        [HttpPost(ApiRoutes.RemoveMember)]
        public async Task<IActionResult> RemoveFromGroup(GroupMemberDTO groupMemberDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await groupService.RemoveFromGroup(groupMemberDTO);
            if (result != null)
                return BadRequest(result);

            #region SignalR
            var userId = User.FindFirst(ClaimNames.UserId)?.Value;
            var connectionsId = await connectionService.GetConnectionsId(userId);
            var username = User.FindFirst(ClaimNames.Username)?.Value;
            var group = groupService.GetGroup(groupMemberDTO.GroupId);
            foreach (var connectionId in connectionsId)
            {
                await hubContext.Groups.RemoveFromGroupAsync(connectionId, group.Name);
            }
            await hubContext.Clients.Groups(group.Name).SendAsync("newGroupEvent", string.Format(ResponseMessages.RemoveFromGroupEvent, username, groupMemberDTO.Username));
            #endregion

            return Ok(string.Format(ResponseMessages.MemberRemoved, groupMemberDTO.Username, groupMemberDTO.GroupId));
        }

    }
}
