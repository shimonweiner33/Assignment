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
                //siteLogger.InsertAsync(LogLevel.Error, 0, $"AccountRepository-IsValidUser, TZ:{loginModel.UserName}, Exception: {ex.ToString()}");
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
    }
}
