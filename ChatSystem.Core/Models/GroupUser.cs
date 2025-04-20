using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Models
{
    public class GroupUser
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Group Group { get; set; }

    }
}
