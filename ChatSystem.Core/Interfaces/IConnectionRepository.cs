using ChatSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Interfaces
{
    public interface IConnectionRepository: IBaseRepository<Connection>
    {
        Connection GetByConnectionId(string connectionId); 
        IList<string> GetConnectionsIdByUserId (string userId);
    }
}
