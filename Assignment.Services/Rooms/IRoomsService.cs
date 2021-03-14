using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services.Rooms
{
    public interface IRoomsService
    {
        Task<int> CreateOrUpdateRoom(Room post);
        Task<bool> DeleteRoom(int roomId);
    }
}
