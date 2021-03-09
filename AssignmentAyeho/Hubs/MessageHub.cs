using Assignment.Data.Repository.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Hubs
{
    public class MessageHub : Hub
    {
        private IConnectionsRepository _connectionsRepository;
        public MessageHub(IConnectionsRepository connectionsRepository)
        {
            this._connectionsRepository = connectionsRepository;
        }
        public Task Send(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }
        //public void Send(string userId, string name, string message)
        //{
        //    Clients.User(userId).SendAsync(name, "message 1");
        //    Clients.Caller.SendAsync(name, "message 2");
        //}
        public Task UpdatePost(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }
        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.Identity.Name;
            if (userName != null)
            {
                await _connectionsRepository.UpdateConnectionId(userName, Context.ConnectionId);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = Context.User.Identity.Name;
            if (userName != null)
            {
                await _connectionsRepository.DeleteConnectionId(userName, Context.ConnectionId);
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
