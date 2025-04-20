using ChatSystem.Core.DTOs;


namespace ChatSystem.Core.Services.Interfaces
{
    public interface IFriendShipService
    {
        public Task<string> SendFriendRequest(string userId ,FriendDTO friendDTO);
        public Task<string> AcceptFriendRequest(string userId, FriendDTO friendDTO);
        public Task<string> RejectFreindRequest(string userId, FriendDTO friendDTO);
        public List<RequestDTO> GetAllPendingRequests(string userId);
        public List<RequestDTO> GetAllAcceptedRequests(string userId);

    }
}
