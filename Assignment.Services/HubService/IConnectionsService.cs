using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services.Connections
{
    public interface IConnectionsService
    {
        Task<ExtendMember> UpdateConnectionId(string userName, string onnectionId);
        Task<bool> DeleteConnectionId(string userName, string connectionId);
        Task<ExtendMembers> GetAllLogInUsers();
        Task<ExtendMember> GetUserConnection(string userName);
    }
}
