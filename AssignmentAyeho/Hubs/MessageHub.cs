//using Assignment.Data.Repository.Interface;
//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using Assignment.Services.Connections;

//namespace Assignment.Hubs
//{
//    public class MessageHub : Hub
//    {
//        private IConnectionsService _connectionsService;
//        private IMemberRepository memberRepository;

//        public MessageHub(IMemberRepository memberRepository, IConnectionsService connectionsService)
//        {
//            this._connectionsService = connectionsService;
//            this.memberRepository = memberRepository;

//        }
//        public Task Send(string message)
//        {
//            return Clients.All.SendAsync("Send", message);
//        }
//        //public void Send(string userId, string name, string message)
//        //{
//        //    Clients.User(userId).SendAsync(name, "message 1");
//        //    Clients.Caller.SendAsync(name, "message 2");
//        //}
//        public Task UpdatePost(string message)
//        {
//            return Clients.All.SendAsync("Send", message);
//        }
//        [Authorize]
//        public override async Task OnConnectedAsync()
//        {
//            //var username2 = Context.GetHttpContext().Request.Query["username"];
//            var userName = Context.User.Identity.Name;
//            if (userName != null)
//            {
//                await _connectionsService.UpdateConnectionId(userName, Context.ConnectionId);
//                var extendMember = await _connectionsService.GetUserConnection(userName);
//                await Clients.All.SendAsync("NewUserLogIn", extendMember);
//                await base.OnConnectedAsync();
//            }


//            //await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
//        }

//        [Authorize]
//        public override async Task OnDisconnectedAsync(Exception exception)
//        {
//            var userName = Context.User.Identity.Name;
//            if (userName != null)
//            {
//                await _connectionsService.DeleteConnectionId(userName, Context.ConnectionId);
//                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");

//                var extendMember = await _connectionsService.GetUserConnection(userName);

//                await Clients.All.SendAsync("UserLogOut", extendMember);

//                await base.OnDisconnectedAsync(exception);
//            }
//        }
//    }
//}
