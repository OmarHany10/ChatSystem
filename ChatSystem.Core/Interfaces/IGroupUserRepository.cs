using ChatSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Interfaces
{
    public interface IGroupUserRepository: IBaseRepository<GroupUser>
    {
        public IList<GroupUser> GetAllUserInGroup(int groupID);
        public IList<string> GetAllUserGroups(string userId);
        public bool IsInGroup(int groupID, string userId);
    }
}
