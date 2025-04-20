using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatSystem.Core.DTOs
{
    public class MessageDTO
    {
        public string Text {  get; set; }

        public int ConversationId { get; set; }

        public string? SenderUsername { get; set; }
    }
}
