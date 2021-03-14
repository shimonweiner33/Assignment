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
        Task<bool> DeleteRoom(int roomId);
    }
}
