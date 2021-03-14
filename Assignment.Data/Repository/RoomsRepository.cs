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



        public async Task<int> CreateOrUpdateRoom(Room room)
        {
            try
            {
                var sQuery = $@"DECLARE @InsertedId int = @roomId;
                                IF EXISTS (SELECT * FROM Rooms
                                WHERE RoomNum = @roomNum)

                                    BEGIN
                                       UPDATE Rooms
                                       SET RoomName = @roomName, 
                                           UserNameOne = @userNameOne, 
                                           UserNameTwo = @userNameTwo,
                                           UpdatedOn = @now, 
                                           UpdatedBy = @user
                                       WHERE RoomNum = @roomNum
                                    END
                                ELSE
                                    BEGIN
                                        INSERT INTO Rooms(RoomName, UserNameOne, UserNameTwo, UpdatedOn, UpdatedBy, CreatedOn, CreatedBy)
                                        VALUES(@roomName, @userNameOne, @userNameTwo, @now, @user, @now, @user);
                                        SELECT @InsertedId = SCOPE_IDENTITY()
                                    END
                                SELECT @InsertedId;";

                using (IDbConnection conn = Connection)
                {
                    var affectedRowId = await conn.ExecuteScalarAsync(sQuery,
                                    new
                                    {
                                        roomNum = room.RoomNum,
                                        roomName = room.RoomName,
                                        userNameOne = room.UserNameOne,
                                        userNameTwo = room.UserNameTwo,
                                        now = DateTime.Now,
                                        user = "user"
                                    });
                    int insertedId = (int)(affectedRowId);
                    return insertedId;
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
                var sQuery = $@"if EXISTS (SELECT * FROM Rooms
                                WHERE RoomNum = @roomNum)
                                    begin
                                       DELETE FROM Rooms WHERE Id = @roomNum
                                    end";

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
