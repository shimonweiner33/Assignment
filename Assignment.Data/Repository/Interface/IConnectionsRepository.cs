using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Repository.Interface
{
    public interface IConnectionsRepository
    {
        Task<bool> UpdateConnectionId(string userName, string onnectionId);
        Task<bool> DeleteConnectionId(string userName, string connectionId);
    }
}
