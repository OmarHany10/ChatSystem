using ChatSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Interfaces
{
    public interface IGropRepository : IBaseRepository<Group>
    {
        public IEnumerable<Message> GetAllMessageByGroupId(int groupId);
        public Group GetByName(string name);
    }
}
