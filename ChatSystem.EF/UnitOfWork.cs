using ChatSystem.Core;
using ChatSystem.Core.Interfaces;
using ChatSystem.EF.Context;
using ChatSystem.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContxt contxt;

        public UnitOfWork(ApplicationDbContxt contxt)
        {
            this.contxt = contxt;
            FriendShips = new FriendShipRepository(contxt);
            Grop = new GroupRepository(contxt);
            GroupUser = new GroupUserRepository(contxt);
            Conversation = new ConversationRepository(contxt);
            Message = new MessagerRepository(contxt);
            Connection = new ConnectionRepository(contxt);
        }
        public IFriendShipRepository FriendShips { get; private set; }

        public IGropRepository Grop { get; private set; }

        public IGroupUserRepository GroupUser { get; private set; }

        public IConversationRepository Conversation { get; private set; }

        public IMessageRepository Message { get; private set; }

        public IConnectionRepository Connection { get; private set; }

        public void Dispose()
        {
            contxt.Dispose();
        }

        public int Save()
        {
            return contxt.SaveChanges();
        }
    }
}
