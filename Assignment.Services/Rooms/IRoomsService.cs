using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services.Rooms
{
    public interface IRoomsService
    {
        Task<Room> CreateOrUpdateRoom(Room post);
        Task<bool> DeleteRoom(int roomId);
        Task<RoomsList> GetAllRooms(string userName);
        Task<Room> GetRoom(int roomId);

    }
}
