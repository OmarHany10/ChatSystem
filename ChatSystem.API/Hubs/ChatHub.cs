using ChatSystem.Core;
using ChatSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatSystem.API.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IUnitOfWork unitOfWork;

        public ChatHub(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public override async Task<Task> OnConnectedAsync()
        {
            var userId = Context.User.FindFirst("uid")?.Value;
            var connectionId = Context.ConnectionId;
            //Console.WriteLine($"Connected: {Context.ConnectionId}, {userId}");


            var connection = new Connection
            {
                ApplicationUserId = userId,
                ConnectionId = connectionId
            };
            unitOfWork.Connection.Add(connection);
            unitOfWork.Save();

            var groupsName = unitOfWork.GroupUser.GetAllUserGroups(userId);
            foreach (var groupName in groupsName)
            {
                await Groups.AddToGroupAsync(connectionId, groupName.ToString());
            }

            //Console.WriteLine($"Connected: {Context.ConnectionId}, {userId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connection = unitOfWork.Connection.GetByConnectionId(Context.ConnectionId);
            unitOfWork.Connection.Delete(connection);
            unitOfWork.Save();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
