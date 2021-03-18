using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Assignment.Data.Models;
using Assignment.Data.Repository.Interface;
using System.Linq;

namespace Assignment.Data.Repository
{
    public class ConnectionsRepository : BaseRepository, IConnectionsRepository
    {
        public ConnectionsRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<bool> UpdateConnectionId(string userName, string connectionId)
        {
            var isUpdated = false;
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = $@"IF EXISTS (SELECT * FROM Connections
                                    WHERE UserName = @userName)

                                        BEGIN
                                           UPDATE Connections
                                           SET UserConnectinonId = @userConnectinonId, 
                                               UpdatedOn = @updatedOn, 
                                               UpdatedBy = @updatedBy
                                           WHERE UserName = @userName
                                        END
                                    ELSE
                                        BEGIN                       
                                           INSERT INTO Connections(UserName, UserConnectinonId, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy)
                                           VALUES (@userName, @userConnectinonId, @createdOn, @createdBy, @updatedOn, @updatedBy)
                                        END";

                    conn.Open();
                    var result = await conn.ExecuteAsync(sQuery,
                        new
                        {
                            userName = userName,
                            userConnectinonId = connectionId,
                            createdOn = DateTime.Now,
                            createdBy = "manager",
                            updatedOn = DateTime.Now,
                            updatedBy = "manager"
                        });

                    isUpdated = true;

                    return isUpdated;
                }
            }
            catch (Exception ex)
            {
                //siteLogger.InsertAsync(LogLevel.Error, 0, $"ConnectionsRepository-UpdateConnectionId, Exception: {ex.ToString()}");
                throw;
            }
        }
        public async Task<bool> DeleteConnectionId(string userName, string connectionId)
        {
            try
            {
                var sQuery = $@"DELETE FROM Connections WHERE UserName = @userName AND UserConnectinonId = @connectionId";

                using (IDbConnection conn = Connection)
                {
                    var affectedRows = await conn.ExecuteAsync(sQuery,
                                    new
                                    {
                                        userName = userName,
                                        connectionId = connectionId
                                    });
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ExtendMembers> GetAllLogInUsers()
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = @"SELECT m.UserName, FirstName, LastName, PhoneNumber, PhoneArea, con.UserConnectinonId FROM Member AS m INNER JOIN Connections AS con 
                                   ON m.UserName = con.UserName";

                    conn.Open();
                    var members = (await conn.QueryAsync<ExtendMember>(sQuery)).ToList();
                    return new ExtendMembers() { Members = members };
                }
            }
            catch (Exception ex)
            {
                //siteLogger.InsertAsync(LogLevel.Error, 0, $"ConnectionsRepository-GeAllLogInUsers, Exception: {ex.ToString()}");
                throw;
            }
        }

        public async Task<ExtendMember> GetUserConnection(string userName)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = @"SELECT m.UserName, FirstName, LastName, PhoneNumber, PhoneArea, con.UserConnectinonId FROM Member AS m 
                                   LEFT JOIN Connections AS con 
                                   ON m.UserName = con.UserName WHERE m.UserName = @userName";
                                   //SELECT m.UserName, FirstName, LastName, PhoneNumber, PhoneArea, con.UserConnectinonId FROM Member AS m INNER JOIN Connections AS con
                                   //ON m.UserName = con.UserName WHERE con.UserName = @userName";

                    conn.Open();

                    var member = (await conn.QueryAsync<ExtendMember>(sQuery, new
                    {
                        userName
                    })).FirstOrDefault();

                    return member;
                }
            }
            catch (Exception ex)
            {
                //siteLogger.InsertAsync(LogLevel.Error, 0, $"ConnectionsRepository-GeAllLogInUsers, Exception: {ex.ToString()}");
                throw;
            }
        }
    }
}
