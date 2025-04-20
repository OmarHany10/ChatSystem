using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Models
{
    public class PrivateConversation : Conversation
    {
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }

        public ApplicationUser FirstUser { get; set; }
        public ApplicationUser SecondUser { get; set; }
    }
}
