using ChatSystem.Core.DTOs;
using ChatSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Services.Interfaces
{
    public interface IGroupService
    {
        public Group GetGroup(int id);
        public Task<string> CreateGroup(GroupDTO groupDTO, string username);
        public Task<string> AddToGroup(GroupMemberDTO groupMemberDTO);
        public IList<string> GetAllUserInGroup(int groupId);
        public Task<string> RemoveFromGroup(GroupMemberDTO groupMemberDTO);

    }
}
