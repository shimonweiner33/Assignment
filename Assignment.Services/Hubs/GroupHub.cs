using Assignment.Data.Repository.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Assignment.Services.Hubs
{
    //[HubName("GroupChatHub")]
    public class GroupHub : Hub
    {
        //private IConnectionsRepository _connectionsRepository;
        //public GroupHub(IConnectionsRepository connectionsRepository)
        //{
        //    this._connectionsRepository = connectionsRepository;
        //}
        //public Task Send(string message)
        //{
        //    return Clients.All.SendAsync("Send", message);
        //}
        ////public void Send(string userId, string name, string message)
        ////{
        ////    Clients.User(userId).SendAsync(name, "message 1");
        ////    Clients.Caller.SendAsync(name, "message 2");
        ////}


        //public Task UpdatePost(string message)
        //{
        //    return Clients.All.SendAsync("Send", message);
        //}

        //[HubMethodName("groupconnect")]
        //public void Get_Connect(String username, String userid, String connectionid, String GroupName)
        //{
        //    string count = "NA";
        //    string msg = "Welcome to group " + GroupName;
        //    string list = "";

        //    var id = Context.ConnectionId;
        //    Groups.Add(id, GroupName);//this will add the connected user to particular group

        //    string[] Exceptional = new string[1];
        //    Exceptional[0] = id;

        //    Clients.Caller.receiveMessage("Group Chat Hub", msg, list);
        //    Clients.OthersInGroup(GroupName).receiveMessage("NewConnection", GroupName + " " + username + " " + id, count);
        //    //Clients.AllExcept(Exceptional).receiveMessage("NewConnection", username + " " + id, count);
        //}


        //[Authorize]
        //public override async Task OnConnectedAsync()
        //{
        //    var username2 = Context.GetHttpContext().Request.Query["username"];
        //    var userName = Context.User.Identity.Name;
        //    if (userName != null)
        //    {
        //        await _connectionsRepository.UpdateConnectionId(userName, Context.ConnectionId);
        //    }
        //    await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
        //    await base.OnConnectedAsync();
        //}

        //[Authorize]
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    var userName = Context.User.Identity.Name;
        //    if (userName != null)
        //    {
        //        await _connectionsRepository.DeleteConnectionId(userName, Context.ConnectionId);
        //    }
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}
