using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.Constants
{
    public static class ApiRoutes
    {
        public const string Register = "Register";
        public const string Login = "Login";
        public const string MyProfilePicture = "MyProfilePicture";
        public const string MyDetails = "MyDetails";

        public const string MyFriends = "MyFriends";
        public const string MyPending = "MyPending";
        public const string SendRequest = "SendRequest";
        public const string AcceptRequest = "AcceptRequest";
        public const string RejectRequest = "RejectRequest";

        public const string GetAllMembers = "GetAllMembers";
        public const string AddMember = "AddMember";
        public const string RemoveMember = "RemoveMember";

        public const string GroupRoute = "Group";
        public const string PrivateRoute = "Private";
    }
}
