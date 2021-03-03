using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Hubs
{
    public class MessageHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }
        public Task UpdatePost(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }
    }
}
