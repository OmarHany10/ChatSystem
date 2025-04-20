using ChatSystem.Core.Interfaces;
using ChatSystem.Core.Models;
using ChatSystem.EF.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ChatSystem.EF.Repositories
{
    public class GroupUserRepository : BaseRepository<GroupUser>, IGroupUserRepository
    {
        private readonly ApplicationDbContxt contxt;

        public GroupUserRepository(ApplicationDbContxt contxt) : base(contxt)
        {
            this.contxt = contxt;
        }

        public IList<string> GetAllUserGroups(string userId)
        {
            return contxt.GroupUsers.Where(gu => gu.UserId == userId).Select(gu => gu.Group.Name).ToList();
        }

        public IList<GroupUser> GetAllUserInGroup(int groupID)
        {
            return contxt.GroupUsers.Where(gu => gu.GroupId == groupID).Include(gu => gu.ApplicationUser).ToList();

        }

        public bool IsInGroup(int groupID, string userId)
        {
            return contxt.GroupUsers.FirstOrDefault(gu => gu.GroupId == groupID && gu.UserId == userId) != null;
        }
    }
}
