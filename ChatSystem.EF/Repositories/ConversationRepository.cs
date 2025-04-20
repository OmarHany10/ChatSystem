using ChatSystem.Core.Interfaces;
using ChatSystem.Core.Models;
using ChatSystem.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.EF.Repositories
{
    public class ConversationRepository : BaseRepository<Conversation>, IConversationRepository
    {
        private readonly ApplicationDbContxt contxt;

        public ConversationRepository(ApplicationDbContxt contxt) : base(contxt)
        {
            this.contxt = contxt;
        }

        public Conversation GetGroupConversationId(int groupId)
        {
            return contxt.Conversations.OfType<GroupConversation>().FirstOrDefault(gc => gc.GroupId == groupId);
        }

        public Conversation GetPrivateConversationId(string firstUserId, string secondUserId)
        {
            return contxt.Conversations.OfType<PrivateConversation>().FirstOrDefault(pc => (pc.FirstUserId == firstUserId && pc.SecondUserId == secondUserId) || (pc.FirstUserId == secondUserId && pc.SecondUserId == firstUserId));
        }

        public Conversation GetPrivateConversationIdIncludeMessages(string firstUserId, string secondUserId)
        {
            return  contxt.Conversations.OfType<PrivateConversation>().Where(pc => (pc.FirstUserId == firstUserId && pc.SecondUserId == secondUserId) || (pc.FirstUserId == secondUserId && pc.SecondUserId == firstUserId)).Include(pc => pc.Messages).FirstOrDefault();
            
        }
    }
}
