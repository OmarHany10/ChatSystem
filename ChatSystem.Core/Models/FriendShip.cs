using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Models
{
    public class FriendShip
    {
        public string UserId { get; set; }
        public string FriendID { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("FriendID")]
        public ApplicationUser Friend { get; set; }

        public bool IsAccepted { get; set; } = false;

        public DateTime SentDate { get; set; }
    }
}
