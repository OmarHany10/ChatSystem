using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Models
{
    public class UserConversation
    {
        public string UserId { get; set; }
        public int ConversationId { get; set; }

        public ApplicationUser User { get; set; }
        public Conversation Conversation { get; set; }
    }
}
