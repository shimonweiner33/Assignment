using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Repository.Interface
{
    public interface IRoomsRepository
    {
        Task<int> CreateOrUpdateRoom(Room room);
        Task<Room> GetRoom(int roomNum);
        Task<RoomsList> GetAllRooms(string userName);
        Task<bool> DeleteRoom(int roomNum);
        Task<List<Rooms_UserConnectinons>> GetRoom_UserConnectinons(int roomNum);
        Task<bool> UpdateConnectionGroupId(string userName, string connectionId);
    }
}
