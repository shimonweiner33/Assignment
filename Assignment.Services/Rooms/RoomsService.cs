using Assignment.Data.Models;
using Assignment.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<int> CreateOrUpdateRoom(Room room)
        {
            var result = await roomsRepository.CreateOrUpdateRoom(room);
            return result;
        }

        public async Task<bool> DeleteRoom(int roomId)
        {
            bool result = await roomsRepository.DeleteRoom(roomId);
            return result;
        }
    }
}
