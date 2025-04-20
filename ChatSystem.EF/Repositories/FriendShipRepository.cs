using ChatSystem.Core.Interfaces;
using ChatSystem.Core.Models;
using ChatSystem.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.EF.Repositories
{
    public class FriendShipRepository : BaseRepository<FriendShip>, IFriendShipRepository
    {
        private readonly ApplicationDbContxt contxt;

        public FriendShipRepository(ApplicationDbContxt contxt) : base(contxt)
        {
            this.contxt = contxt;
        }

        public IEnumerable<FriendShip> GetAllAccepted(string userId)
        {
            
            return contxt.Friendships.Where(f => (f.UserId == userId || f.FriendID == userId)  && f.IsAccepted == true).Include(f => f.Friend).Include(f => f.User);
        }

        public IEnumerable<FriendShip> GetAllPending(string userId)
        {
            return contxt.Friendships.Where(f => f.FriendID == userId && f.IsAccepted == false).Include(f => f.User);
        }

        public FriendShip GetRequest(string userId, string friendId)
        {
            return contxt.Friendships.FirstOrDefault(f => (f.UserId == userId && f.FriendID == friendId) || (f.UserId == friendId && f.FriendID == userId));
        }


    }
}
