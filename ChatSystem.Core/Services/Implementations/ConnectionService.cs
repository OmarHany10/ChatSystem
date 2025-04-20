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
    public class ConnectionService : IConnectionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public ConnectionService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<IList<string>> GetConnectionsId(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            return unitOfWork.Connection.GetConnectionsIdByUserId(user.Id).ToList();
        }

        
    }
}
