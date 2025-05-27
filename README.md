# PulseChat API

ChatSystem is a real-time chat backend built with **ASP.NET Core Web API**, **SignalR**, and **Entity Framework Core**. It supports full user authentication, friend management, group and private messaging, and real-time communication via SignalR.

---

## ✅ Features

- JWT-based Authentication with Refresh Tokens
- Secure API endpoints with [Authorize]
- Upload and retrieve user profile pictures
- Friend request system: Send, Accept, Reject
- Group chat creation and member management
- Real-time messaging via SignalR (private & group)
- Automatic SignalR group assignments on connect
- Clean architecture with service/repository layers

---

## ⚙️ Tech Stack

- **ASP.NET Core**
- **Entity Framework Core + SQL Server**
- **SignalR** for real-time WebSocket-based communication
- **JWT Authentication** with refresh tokens
- **Swagger** for API documentation and testing

---

## 📁 Project Structure

```
ChatSystem.API/
├── Controllers/
│ ├── AuthController.cs # Login, Register, Profile Picture
│ ├── FriendController.cs # Friend Request Logic
│ ├── GroupController.cs # Group Chat Logic
│ └── MessageController.cs # Messaging Endpoints
├── Hubs/
│ └── ChatHub.cs # SignalR Hub Logic
├── Program.cs # Main DI + SignalR + JWT Config
├── appsettings.json # Configuration

ChatSystem.Core/
├── Constants/
│ ├── ApiRoutes.cs
│ ├── ClaimNames.cs
│ └── ResponseMessages.cs
├── DTOs/ # Data Transfer Objects
├── Helpers/
│ └── JWT.cs # JWT configuration model
├── Interfaces/
│ ├── Repository Interfaces
│ └── Service Interfaces
├── Models/
│ └── Domain models (User, Group, Message, etc.)
├── Services/
│ ├── Interfaces/
│ └── Implementations/

ChatSystem.EF/
├── Context/
│ └── ApplicationDbContxt.cs # DB Context
├── Migrations/ # EF Core migration history
├── Repositories/ # Concrete Repositories
├── UnitOfWork.cs # Unit of Work Pattern
```

---

## 🔐 Authentication Endpoints

- `POST /api/auth/register` — Register a new user  
- `POST /api/auth/login` — Login and receive JWT & refresh token  
- `GET /api/auth/refresh` — Refresh the JWT token  
- `GET /api/auth/revoke` — Revoke the current refresh token  
- `POST /api/auth/my-picture` — Upload profile picture  
- `GET /api/auth/my-picture` — Retrieve profile picture  
- `GET /api/auth/my-details` — Retrieve current user info  

---

## 👥 Friend System

- `GET /api/friend/my-friends` — List accepted friends  
- `GET /api/friend/my-pending` — List pending friend requests  
- `POST /api/friend/send-request` — Send friend request  
- `POST /api/friend/accept-request` — Accept friend request  
- `POST /api/friend/reject-request` — Reject friend request  

---

## 🧑‍🤝‍🧑 Group Chat

- `POST /api/group` — Create a new group  
- `GET /api/group/get-members?groupId=1` — List members in a group  
- `POST /api/group/add-member` — Add a user to group  
- `POST /api/group/remove-member` — Remove a user from group  

---

## 💬 Messaging

- `GET /api/message/{groupId:int}` — Get all messages in a group chat  
- `GET /api/message/{friendUsername}` — Get private chat messages  
- `POST /api/message/send-group` — Send message to group  
- `POST /api/message/send-private` — Send private message  

---

## 🔌 SignalR Setup

- **Endpoint:** `/chatHub`
- **Features:**
  - On user connection: assigns user to SignalR groups
  - On disconnection: removes connection from DB
  - Events:
    - `newGroupEvent`
    - `NewGroupMessageMethod`
    - `NewPrivateMessageMethod`

#### 🔧 SignalR Client Example (JavaScript)

```javascript
const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:<port>/chatHub", {
    accessTokenFactory: () => token
  })
  .build();

connection.on("newGroupEvent", (msg) => console.log("Group:", msg));

await connection.start();
```

---

## 🚀 Getting Started

### Prerequisites

- .NET 6 SDK or later  
- SQL Server instance  
- Node.js (optional for frontend)

### Setup Instructions

1. **Clone the repository:**
```bash
git clone https://github.com/your-repo/chat-system-api.git
```

2. **Configure `appsettings.json`** with your SQL connection string and JWT keys:
```json
{
  "ConnectionStrings": {
    "cs": "Your-SQL-Server-Connection-String"
  },
  "JWT": {
    "Key": "Your-JWT-Secret-Key",
    "Issuer": "YourAppIssuer",
    "Audience": "YourAppAudience"
  }
}
```

3. **Apply EF Core migrations:**
```bash
dotnet ef database update
```

4. **Run the application:**
```bash
dotnet run
```

5. **Access Swagger UI:**
```
https://localhost:<port>/swagger
```

---

## 🧪 Testing with Swagger

Swagger UI is enabled in development by default. Use it to:

- ✅ Register and login users  
- 🔑 Test endpoints securely with JWT  
- 💬 Simulate chat actions (create group, send message, etc.)

---

## 👨‍💻 Author

Built by Omar Hany. Contributions, issues, and suggestions are welcome!
