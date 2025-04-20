using ChatSystem.Core.DTOs;
using ChatSystem.Core.Models;
using ChatSystem.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace ChatSystem.Core.Services.Implementations
{
    public class FriendShipService : IFriendShipService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public FriendShipService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<string> AcceptFriendRequest(string userId, FriendDTO friendDTO)
        {
            var friend = await userManager.FindByNameAsync(friendDTO.friendUsername);


            if (friend == null)
                return "There are no user have this username";

            var request = unitOfWork.FriendShips.GetRequest(friend.Id, userId);

            if (request == null)
                return "There are no friend request from this user";
            else if (request.IsAccepted == true)
                return "This request already accepted";

            request.IsAccepted = true;
            unitOfWork.FriendShips.Update(request);
            unitOfWork.Save();
            var user = await userManager.FindByIdAsync(userId);
            var privateConversation = new PrivateConversation
            {
                Name = $"{user.UserName} and {friendDTO.friendUsername} chat",
                FirstUserId = userId,
                SecondUserId = friend.Id,
            };
            unitOfWork.Conversation.Add(privateConversation);
            unitOfWork.Save();
            return null;
        }

        public List<RequestDTO> GetAllAcceptedRequests(string userId)
        {
            var acceptedRequests = unitOfWork.FriendShips.GetAllAccepted(userId).ToList();

            var result = new List<RequestDTO>();
            foreach (var acceptedRequest in acceptedRequests)
            {
                if (userId == acceptedRequest.FriendID)
                {
                    result.Add(new RequestDTO
                    {
                        Username = acceptedRequest.User.UserName,
                        FirstName = acceptedRequest.User.FirstName,
                        LastName = acceptedRequest.User.LastName,
                        SentDate = acceptedRequest.SentDate,
                    });
                }
                else
                {
                    result.Add(new RequestDTO
                    {
                        Username = acceptedRequest.Friend.UserName,
                        FirstName = acceptedRequest.Friend.FirstName,
                        LastName = acceptedRequest.Friend.LastName,
                        SentDate = acceptedRequest.SentDate,
                    });
                }
            }
            return result;
        }

        public List<RequestDTO> GetAllPendingRequests(string userId)
        {
            var pendingRequests = unitOfWork.FriendShips.GetAllPending(userId).ToList();
            
            return ConvertToDTO(pendingRequests);
        }

        public async Task<string> RejectFreindRequest(string userId, FriendDTO friendDTO)
        {
            var friend = await userManager.FindByNameAsync(friendDTO.friendUsername);

            if (friend == null)
                return "There are no user have this username";

            var request = unitOfWork.FriendShips.GetRequest(userId, friend.Id);

            if (request == null)
                return "There are no friend request from this user";

            unitOfWork.FriendShips.Delete(request);
            unitOfWork.Save();
            return null;
        }

        public async Task<string> SendFriendRequest(string userId, FriendDTO friendDTO)
        {
            var user = await userManager.FindByIdAsync(userId);
            var friend = await userManager.FindByNameAsync(friendDTO.friendUsername);
            if (friend == null)
                return "Incorrect Friend Username";

            if (unitOfWork.FriendShips.GetRequest(userId, friend.Id) != null || unitOfWork.FriendShips.GetRequest(friend.Id ,userId) != null)
                return "Request are already sent";

            var friendShip = new FriendShip
            {
                UserId = userId,
                FriendID = friend.Id,
                SentDate = DateTime.Now,
                IsAccepted = false,
            };

            unitOfWork.FriendShips.Add(friendShip);
            unitOfWork.Save();
            return null;
        }

        public List<RequestDTO> ConvertToDTO(List<FriendShip> friendShips)
        {
            var result = new List<RequestDTO>();
            foreach (var friendShip in friendShips)
            {

                result.Add(new RequestDTO
                {
                    Username = friendShip.User.UserName,
                    FirstName = friendShip.User.FirstName,
                    LastName = friendShip.User.LastName,
                    SentDate = friendShip.SentDate,
                });
            }
            return result;
        }
    }
}
