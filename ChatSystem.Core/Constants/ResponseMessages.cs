using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Constants
{
    public static class ResponseMessages
    {
        public const string PictureUploadSuccess = "Picture uploaded successfully";
        public const string PictureUploadFailed = "Failed to upload picture";

        public const string RequestSent = "Request Sent";
        public const string RequestAccepted = "Accept Request";
        public const string RequestRejected = "Reject Request";

        public const string GroupCreated = "{0} Created Successfully";
        public const string MemberAdded = "{0} added to group with group id => {1}";
        public const string MemberRemoved = "{0} remove from group with group id => {1}";

        public const string CreateGroupEvent = "{0} created this group";
        public const string AddToGroupEvent = "{0} Added {1} to the group";
        public const string RemoveFromGroupEvent = "{0} Removed {1} from the group";

        public const string SuccessMessage = "Message sent successfully";
        public const string NewGroupMessageMethod = "newGroupMessage";
        public const string NewPrivateMessageMethod = "newPrivateMessage";
    }
}
