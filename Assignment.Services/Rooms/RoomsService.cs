using Assignment.Data.Models;
using Assignment.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignment.Services.Rooms
{
    public class RoomsService : IRoomsService
    {
        private IRoomsRepository roomsRepository;
        public RoomsService(IRoomsRepository roomsRepository)
        {
            this.roomsRepository = roomsRepository;
        }

        public async Task<Room> CreateOrUpdateRoom(Room room)
        {
            Room insertedRoom = null;
            var result = await roomsRepository.CreateOrUpdateRoom(room);

            if (result != 0)
            {
                insertedRoom = await GetRoom(result);
            }
            return insertedRoom;
        }
        public async Task<RoomsList> GetAllRooms(string userName)
        {
            var rooms = await roomsRepository.GetAllRooms(userName);

            return rooms;
        }

        public async Task<Room> GetRoom(int roomId)
        {
            Room room = await roomsRepository.GetRoom(roomId);
            room.UserConnectinons = await roomsRepository.GetRoom_UserConnectinons(roomId);
            return room;
        }

        public async Task<bool> DeleteRoom(int roomId)
        {
            bool result = await roomsRepository.DeleteRoom(roomId);
            return result;
        }
    }
}
