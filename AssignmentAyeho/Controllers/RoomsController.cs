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

        [Authorize]
        [HttpPost, Route("CreateOrUpdateRoom")]
        public async Task<int> CreateOrUpdateRoom(Room room)
        {
            int InsertedId = 0;
            try
            {
                room.UserNameOne = User.Identity.Name;
                InsertedId = await _roomsService.CreateOrUpdateRoom(room);
                room.RoomNum = InsertedId;
                var userOne = await _connectionsRepository.GetUserConnection(room.UserNameOne);
                var userTwo = await _connectionsRepository.GetUserConnection(room.UserNameTwo);

                await _messageHubContex.Clients.Users(userOne.UserConnectinonId, userTwo.UserConnectinonId).SendAsync("CreateOrUpdateRoom", room);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"CreateOrUpdateRoom('{room}')  failed");
                throw;
            }
            return InsertedId;
        }
    }
}
