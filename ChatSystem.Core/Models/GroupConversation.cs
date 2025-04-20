using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Models
{
    public class GroupConversation: Conversation
    {
        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}
