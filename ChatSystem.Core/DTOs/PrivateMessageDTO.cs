using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.DTOs
{
    public class PrivateMessageDTO
    {
        public string Text { get; set; }
        public string FriendUsername { get; set; }
    }
}
