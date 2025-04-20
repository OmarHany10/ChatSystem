using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.DTOs
{
    public class SendMessageDTO
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int ConversationId { get; set; }
    }
}
