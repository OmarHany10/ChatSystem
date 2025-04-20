using ChatSystem.Core.DTOs;
using ChatSystem.Core.Models;
using ChatSystem.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public MessageService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<List<MessageDTO>> GetMessagesInGroup(int groupId)
        {
            var messages = unitOfWork.Grop.GetAllMessageByGroupId(groupId).ToList();
            var result = new List<MessageDTO>();
            foreach (var message in messages)
            {
                var user = await userManager.FindByIdAsync(message.ApplicationUserId);

                result.Add(new MessageDTO
                {
                    SenderUsername = user.UserName,
                    Text = message.Text,
                    ConversationId = message.ConversationId,
                });
            }
            return result;
        }

        public async Task<List<MessageDTO>> GetMessagesInPrivateChat(string freindUsername, string userId)
        {
            var friend = await userManager.FindByNameAsync(freindUsername);

            var conversation = unitOfWork.Conversation.GetPrivateConversationIdIncludeMessages(userId, friend.Id);
            if (conversation == null || conversation.Messages == null)
                return null;
            var result = new List<MessageDTO>();
            foreach (var message in conversation.Messages)
            {
                var user = await userManager.FindByIdAsync(message.ApplicationUserId);

                result.Add(new MessageDTO
                {
                    SenderUsername = user.UserName,
                    Text = message.Text,
                    ConversationId = message.ConversationId,
                });
            }
            return result;
        }

        public string SendMessage(SendMessageDTO sendMessageDTO, string userId)
        {
            if (unitOfWork.Conversation.GetById(sendMessageDTO.ConversationId) == null)
                return $"There is no Conversation with this ID => {sendMessageDTO.ConversationId}";

            var message = new Message
            {
                Text = sendMessageDTO.Text,
                Date = DateTime.Now,
                ConversationId = sendMessageDTO.ConversationId,
                ApplicationUserId = userId
            };

            unitOfWork.Message.Add(message);
            unitOfWork.Save();
            return null;
        }

        public string SendMessageToGroupConversation(GroupMessageDTO groupMessageDTO, string userId)
        {
            var group = unitOfWork.Grop.GetById(groupMessageDTO.GroupId);
            if (group == null)
                return $"There is no group with this Id => {groupMessageDTO.GroupId}";

            var isInGroup = unitOfWork.GroupUser.IsInGroup(groupMessageDTO.GroupId, userId);
            if (!isInGroup)
                return $"Please join group with this Id => {groupMessageDTO.GroupId} first";

            var conversation = unitOfWork.Conversation.GetGroupConversationId(groupMessageDTO.GroupId);


            var message = new Message
            {
                Text = groupMessageDTO.Text,
                Date = DateTime.Now,
                ConversationId = conversation.Id,
                ApplicationUserId = userId
            };
            unitOfWork.Message.Add(message);
            unitOfWork.Save();
            return null;
        }

        public async Task<string> SendMessageToPrivateConversation(PrivateMessageDTO privateMessageDTO, string userId)
        {
            var friend = await userManager.FindByNameAsync(privateMessageDTO.FriendUsername);

            if (friend == null)
                return "There is no user have this userName";

            var conversation = unitOfWork.Conversation.GetPrivateConversationId(userId, friend.Id);
            if (conversation == null)
                return $"Please sent friend request to {friend.UserName} before you chat him";

            var message = new Message
            {
                Text = privateMessageDTO.Text,
                Date = DateTime.Now,
                ConversationId = conversation.Id,
                ApplicationUserId = userId
            };
            unitOfWork.Message.Add(message);
            unitOfWork.Save();
            return null;
        }
    }
}
