using ChatSystem.Core.DTOs;
using ChatSystem.Core.Services.Interfaces;
using ChatSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChatSystem.Core.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public GroupService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public async Task<string> AddToGroup(GroupMemberDTO groupMemberDTO)
        {
            var user = await userManager.FindByNameAsync(groupMemberDTO.Username);
            if (user == null)
                return "No user have this username";

            var group = unitOfWork.Grop.GetById(groupMemberDTO.GroupId);
            if (group == null)
                return "No Group have this id";

            var isInGroup = unitOfWork.GroupUser.IsInGroup(groupMemberDTO.GroupId, user.Id);
            if (isInGroup)
                return "User is already in group";


            var gropUser = new GroupUser
            {
                GroupId = groupMemberDTO.GroupId,
                UserId = user.Id,
            };
            unitOfWork.GroupUser.Add(gropUser);
            unitOfWork.Save();
            return null;
        }

        public async Task<string> CreateGroup(GroupDTO groupDTO, string username)
        {
            var group = unitOfWork.Grop.GetByName(groupDTO.Name);
            if (group != null)
                return $"Group {group.Name} is already created";
            var newGroup = new Group 
            {
                Name = groupDTO.Name,
                CreatedBy = username
            };
            unitOfWork.Grop.Add(newGroup);
            unitOfWork.Save();
            var groupConversation = new GroupConversation
            {
                GroupId = newGroup.Id,
                Name = $"{groupDTO.Name} Conversation",
            };
            unitOfWork.Conversation.Add(groupConversation);
            unitOfWork.Save();
            var addToGroupDTO = new GroupMemberDTO
            {
                GroupId = newGroup.Id,
                Username = username,
            };
            await AddToGroup(addToGroupDTO);
            return null;
        }


        public IList<string> GetAllUserInGroup(int groupId)
        {
            var group = unitOfWork.Grop.GetById(groupId);
            if (group == null)
                return null;
            var users = unitOfWork.GroupUser.GetAllUserInGroup(groupId);
            var result = new List<string>();
            foreach ( var user in users)
                result.Add(user.ApplicationUser.UserName);
            return result;
        }

        public Group GetGroup(int id)
        {
            return unitOfWork.Grop.GetById(id);
        }

        public async Task<string> RemoveFromGroup(GroupMemberDTO groupMemberDTO)
        {
            var user = await userManager.FindByNameAsync(groupMemberDTO.Username);
            if (user == null)
                return "No user have this username";

            var group = unitOfWork.Grop.GetById(groupMemberDTO.GroupId);
            if (group == null)
                return "No Group have this id";

            var isInGroup = unitOfWork.GroupUser.IsInGroup(groupMemberDTO.GroupId, user.Id);
            if (!isInGroup)
                return "User is not in group";

            var gropUser = new GroupUser
            {
                GroupId = groupMemberDTO.GroupId,
                UserId = user.Id,
            };
            
            unitOfWork.GroupUser.Delete(gropUser);
            unitOfWork.Save();
            return null;
        }
    }
}
