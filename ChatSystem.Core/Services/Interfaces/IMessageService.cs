using ChatSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Services.Interfaces
{
    public interface IMessageService
    {
        public string SendMessage(SendMessageDTO sendMessageDTO, string userId);
        public Task<string> SendMessageToPrivateConversation(PrivateMessageDTO privateMessageDTO, string userId);
        public string SendMessageToGroupConversation(GroupMessageDTO groupMessageDTO, string userId);
        public Task<List<MessageDTO>> GetMessagesInGroup(int groupId);
        public Task<List<MessageDTO>> GetMessagesInPrivateChat(string freindUsername, string userId);
    }
}
