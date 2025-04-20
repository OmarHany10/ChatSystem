using ChatSystem.Core.Interfaces;
using ChatSystem.EF.Context;
using ChatSystem.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace ChatSystem.EF.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGropRepository
    {
        private readonly ApplicationDbContxt contxt;

        public GroupRepository(ApplicationDbContxt contxt) : base(contxt)
        {
            this.contxt = contxt;
        }

        public Group GetByName(string name)
        {
            return contxt.Groups.FirstOrDefault(g => g.Name == name);
        }

        public IEnumerable<Message> GetAllMessageByGroupId(int groupId)
        {
            var group = contxt.Groups.Where(g => g.Id == groupId).Include(g => g.GroupConversation.Messages).FirstOrDefault();

            return group.GroupConversation.Messages;
        }
    }
}
