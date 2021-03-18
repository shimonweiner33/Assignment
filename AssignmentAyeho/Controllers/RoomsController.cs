using Assignment.Data.Models;
using Assignment.Services.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Assignment.Services.Rooms;
using Assignment.Data.Repository.Interface;
using System.Text.Json;

namespace Rooms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomsService _roomsService;
        //private readonly ILogger<AssignmentController> _logger;

        private readonly IHubContext<MessageHub> _messageHubContex;
        private IConnectionsRepository _connectionsRepository;

        public RoomsController(IRoomsService roomsService, IHubContext<MessageHub> messageHubContex, IConnectionsRepository connectionsRepository)
        {
            this._connectionsRepository = connectionsRepository;
            this._roomsService = roomsService;
            this._messageHubContex = messageHubContex;
            //_logger = (ILogger<RoomsController>)Log.ForContext<RoomsController>();

        }
        [HttpGet, Route("GetAllRooms")]
        public Task<RoomsList> GetAllRooms()
        {
            try
            {
                string userName = User.Identity.Name;
                return _roomsService.GetAllRooms(userName);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"GetAllPosts('{roomNum}')  failed");
                throw;
            }
        }
        [Authorize]
        [HttpPost, Route("CreateOrUpdateRoom")]
        public async Task<int> CreateOrUpdateRoom(Room room)
        {
            int insertedId = 0;
            try
            {
                room.UserName = User.Identity.Name;

                //add the current user to jsonList todo
                Room insertedRoom = await _roomsService.CreateOrUpdateRoom(room);

                insertedId = insertedRoom.RoomNum;
                room.RoomNum = insertedId;
                foreach (var UserConnectinon in insertedRoom.UserConnectinons)
                {
                    await _messageHubContex.Groups.AddToGroupAsync(UserConnectinon.UserConnectinonId, room.RoomName);
                }
                //await _messageHubContex.Clients.Users(usersRoom).SendAsync("CreateOrUpdateRoom", room);
                await _messageHubContex.Clients.Group(room.RoomName).SendAsync("CreateOrUpdateRoom", room);

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"CreateOrUpdateRoom('{room}')  failed");
                throw;
            }
            return insertedId;
        }
    }
}
