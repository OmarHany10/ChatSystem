using ChatSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Interfaces
{
    public interface IConversationRepository: IBaseRepository<Conversation>
    {
        public Conversation GetPrivateConversationId(string firstUserId, string secondUserId); 
        public Conversation GetPrivateConversationIdIncludeMessages(string firstUserId, string secondUserId); 
        public Conversation GetGroupConversationId(int groupId); 
        
    }
}
