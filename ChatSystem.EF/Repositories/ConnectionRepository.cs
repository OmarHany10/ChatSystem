using ChatSystem.Core.Interfaces;
using ChatSystem.Core.Models;
using ChatSystem.EF.Context;

namespace ChatSystem.EF.Repositories
{
    public class ConnectionRepository : BaseRepository<Connection>, IConnectionRepository
    {
        private readonly ApplicationDbContxt contxt;

        public ConnectionRepository(ApplicationDbContxt contxt) : base(contxt)
        {
            this.contxt = contxt;
        }

        public Connection GetByConnectionId(string connectionId)
        {
            return contxt.Connections.FirstOrDefault(c => c.ConnectionId == connectionId);
        }

        public IList<string> GetConnectionsIdByUserId(string userId)
        {
            return contxt.Connections.Where(c => c.ApplicationUserId == userId).Select(c => c.ConnectionId).ToList();
        }
    }
}
