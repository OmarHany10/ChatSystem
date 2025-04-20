using ChatSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Interfaces
{
    public interface IFriendShipRepository : IBaseRepository<FriendShip>
    {
        public IEnumerable<FriendShip> GetAllPending(string userId);

        public IEnumerable<FriendShip> GetAllAccepted(string userId);
        public FriendShip GetRequest(string userId, string friendId);
    }
}
