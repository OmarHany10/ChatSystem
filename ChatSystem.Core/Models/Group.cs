using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public virtual GroupConversation? GroupConversation { get; set; }

        public virtual ICollection<GroupUser> GroupUsers { get; set; } = new HashSet<GroupUser>();

    }
}
