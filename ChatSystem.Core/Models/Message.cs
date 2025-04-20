using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(ApplicationUser))]
        [DisplayName("SenderId")]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(Conversation))]
        public int ConversationId { get; set; }

        public virtual Conversation Conversation { get; set; }
    }
}
