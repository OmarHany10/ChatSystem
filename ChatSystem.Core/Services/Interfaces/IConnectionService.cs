using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<IList<string>> GetConnectionsId(string username);
    }
}
