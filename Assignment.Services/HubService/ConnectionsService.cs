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
        private IRoomsRepository _roomsRepository;


        public ConnectionsService(IConnectionsRepository connectionsRepository, IRoomsRepository roomsRepository)
        {
            this._connectionsRepository = connectionsRepository;
            this._roomsRepository = roomsRepository;
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

        public async Task<ExtendMember> UpdateConnectionId(string userName, string connectionId)
        {
            ExtendMember newUser = null;
            bool isUpdated = await _connectionsRepository.UpdateConnectionId(userName, connectionId);
            bool isRoomsUpdated = await _roomsRepository.UpdateConnectionGroupId(userName, connectionId);

            if (isUpdated && isRoomsUpdated)
            {
                newUser = await GetUserConnection(userName);
            }
            return newUser;
        }
    }
}
