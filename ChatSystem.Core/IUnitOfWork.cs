using ChatSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core
{
    public interface IUnitOfWork: IDisposable
    {
        IFriendShipRepository FriendShips { get; }
        IGropRepository Grop { get; }
        IGroupUserRepository GroupUser { get; }
        IConversationRepository Conversation { get; }
        IMessageRepository Message { get; }
        IConnectionRepository Connection { get; }
        int Save();
    }
}
