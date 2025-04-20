using JWTRefreshToken.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        public byte[]? Picture { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();

        public virtual ICollection<GroupUser> GroupUsers { get; set; } = new HashSet<GroupUser>();

        public virtual ICollection<FriendShip> Friendships { get; set; } = new HashSet<FriendShip>();

        public virtual ICollection<UserConversation> UserConversations { get; set; } = new HashSet<UserConversation>();
        public virtual ICollection<Connection> Connections { get; set; } = new HashSet<Connection>();
        public IList<RefreshToken> RefreshTokens { get; set; }
    }
}
