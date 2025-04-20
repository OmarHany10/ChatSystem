using ChatSystem.Core.Interfaces;
using ChatSystem.Core.Models;
using ChatSystem.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.EF.Repositories
{
    public class MessagerRepository : BaseRepository<Message>, IMessageRepository
    {
        private readonly ApplicationDbContxt contxt;

        public MessagerRepository(ApplicationDbContxt contxt) : base(contxt)
        {
            this.contxt = contxt;
        }

        public IEnumerable<Message> GetByConversationId(int conversationId)
        {
            return contxt.Messages.Where(m => m.ConversationId == conversationId);
        }
    }
}
