using Assignment.Data.Models;
using Assignment.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Assignment.Services.Hubs;

namespace Assignment.Services.Connections
{
    public class ConnectionsService : IConnectionsService
    {
        private IConnectionsRepository _connectionsRepository;

        public ConnectionsService(IConnectionsRepository connectionsRepository)
        {
            this._connectionsRepository = connectionsRepository;
        }

        public async Task<bool> DeleteConnectionId(string userName, string connectionId)
        {
            bool isRemove = await _connectionsRepository.DeleteConnectionId(userName, connectionId);
            return isRemove;
        }

        public async Task<ExtendMembers> GetAllLogInUsers()
        {
            var users = await _connectionsRepository.GetAllLogInUsers();
            return users;
        }

        public async Task<ExtendMember> GetUserConnection(string userName)
        {
            var user = await _connectionsRepository.GetUserConnection(userName);
            return user;
        }

        public async Task<ExtendMember> UpdateConnectionId(string userName, string onnectionId)
        {
            ExtendMember newUser = null;
            bool isUpdated = await _connectionsRepository.UpdateConnectionId(userName, onnectionId);
            if (isUpdated)
            {
                newUser = await GetUserConnection(userName);
            }
            return newUser;
        }
    }
}
