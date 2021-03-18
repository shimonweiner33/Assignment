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
    public class RoomsRepository : BaseRepository, IRoomsRepository
    {
        public RoomsRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<RoomsList> GetAllRooms(string userName)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = @"
                        SELECT * FROM Rooms AS r INNER JOIN Rooms_UserConnectinons AS ru ON r.RoomNum = ru.RoomNum 
                        WHERE ru.UserName = @userName";
                    conn.Open();
                    var result = (await conn.QueryAsync<Room>(sQuery, new
                    {
                        userName = userName
                    })).ToList();
                    return new RoomsList() { Rooms = result };
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> CreateOrUpdateRoom(Room room)
        {
            try
            {
                var sQuery = $@"DECLARE @InsertedId int = @roomNum;
                                IF EXISTS (SELECT * FROM Rooms
                                WHERE RoomNum = @roomNum)

                                    BEGIN
                                       UPDATE Rooms
                                       SET RoomName = @roomName, 
                                           UpdatedOn = @now, 
                                           UpdatedBy = @user
                                       WHERE RoomNum = @roomNum
                                    END
                                ELSE
                                    BEGIN
                                        INSERT INTO Rooms(ManagerUserName, RoomName, UpdatedOn, UpdatedBy, CreatedOn, CreatedBy)
                                        VALUES(@userName, @roomName, @now, @user, @now, @user);
                                        SELECT @InsertedId = SCOPE_IDENTITY()
                                    END
                                SELECT @InsertedId;";

                using (IDbConnection conn = Connection)
                {
                    var affectedRowId = await conn.ExecuteScalarAsync(sQuery,
                                    new
                                    {
                                        userName = room.UserName,
                                        roomNum = room.RoomNum,
                                        roomName = room.RoomName,
                                        now = DateTime.Now,
                                        user = "user"
                                    });
                    int insertedId = (int)(affectedRowId);

                    // todo - new version of daper there are option to bulk insert
                    foreach (var item in room.Users)
                    {
                        string processQuery = @"IF NOT EXISTS (SELECT * FROM Rooms_UserConnectinons 
                                                WHERE UserConnectinonId = @userConnectinonId AND RoomNum = @roomNum)
                                                BEGIN
                                                   INSERT INTO Rooms_UserConnectinons VALUES (@userConnectinonId, @roomNum, @userName)
                                                END";
                        conn.Execute(processQuery, new
                        {
                            roomNum = insertedId,
                            userConnectinonId = item.UserConnectinonId,
                            userName = item.UserName
                        });
                    }

                    return insertedId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateConnectionGroupId(string userName, string connectionId)
        {
            var isUpdated = false;
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = $@"UPDATE Rooms_UserConnectinons
                                           SET UserConnectinonId = @userConnectinonId
                                           WHERE UserName = @userName";

                    conn.Open();
                    var result = await conn.ExecuteAsync(sQuery,
                        new
                        {
                            userName = userName,
                            userConnectinonId = connectionId
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


        public async Task<Room> GetRoom(int roomNum)
        {
            try
            {
                var sQuery = $@"SELECT * FROM Rooms
                                WHERE RoomNum = @roomNum";

                using (IDbConnection conn = Connection)
                {
                    var result = (await conn.QueryAsync<Room>(sQuery, new
                    {
                        roomNum = roomNum
                    })).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Rooms_UserConnectinons>> GetRoom_UserConnectinons(int roomNum)
        {
            try
            {
                var sQuery = $@"SELECT * FROM Rooms_UserConnectinons
                                WHERE RoomNum = @roomNum";

                using (IDbConnection conn = Connection)
                {
                    var result = (await conn.QueryAsync<Rooms_UserConnectinons>(sQuery, new
                    {
                        roomNum = roomNum
                    })).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteRoom(int roomNum)
        {
            try
            {
                var sQuery = $@"DELETE FROM Rooms_UserConnectinons WHERE RoomNum = @roomNum
                                DELETE FROM Rooms WHERE RoomNum = @roomNum";

                using (IDbConnection conn = Connection)
                {
                    var affectedRows = await conn.ExecuteAsync(sQuery,
                                    new
                                    {
                                        roomNum = roomNum
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
