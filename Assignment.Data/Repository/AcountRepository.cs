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
    public class AcountRepository : BaseRepository, IAcountRepository
    {
        public AcountRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<bool> Register(Register registerDetails)
        {
            var result = false;
            try
            {
                using (IDbConnection conn = Connection)
                {

                    var queryToMember = @"IF NOT EXISTS (SELECT * FROM Member
                                           WHERE UserName = @userName)
                                               begin
                                                  INSERT INTO Member(UserName, FirstName, LastName, PhoneArea, PhoneNumber, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy)
                                                  VALUES (@userName, @firstName, @lastName, @phoneArea, @phoneNumber,  @createdOn, @createdBy,@updatedOn, @updatedBy)
                                               end";

                    conn.Open();
                    var resultToMember = await conn.ExecuteAsync(queryToMember,
                        new
                        {
                            userName = registerDetails.UserName,
                            firstName = string.IsNullOrEmpty(registerDetails.FirstName) ? null:registerDetails.FirstName,
                            lastName = string.IsNullOrEmpty(registerDetails.LastName) ? null:registerDetails.LastName,
                            phoneArea = string.IsNullOrEmpty(registerDetails.PhoneArea)?null:registerDetails.PhoneArea,
                            phoneNumber = string.IsNullOrEmpty(registerDetails.PhoneNumber)?null: registerDetails.PhoneNumber,
                            createdOn = DateTime.Now,
                            createdBy = "manager",
                            updatedOn = DateTime.Now,
                            updatedBy = "manager",
                        });

                    var queryToAcount = @"IF NOT EXISTS (SELECT * FROM Acount
                                           WHERE UserName = @userName)
                                               begin
                                                    INSERT INTO Acount(UserName, PasswordHash, CreatedOn, CreatedBy, UpdatedOn, UpdatedBy)
                                                    VALUES (@userName, @passwordHash, @createdOn, @createdBy, @updatedOn, @updatedBy)
                                               end;";

                    var resultToAcount = await conn.ExecuteAsync(queryToAcount,
                        new
                        {
                            userName = registerDetails.UserName,
                            passwordHash = registerDetails.Password,
                            createdOn = DateTime.Now,
                            createdBy = "manager",
                            updatedOn = DateTime.Now,
                            updatedBy = "manager",
                        });

                    result = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                //siteLogger.InsertAsync(LogLevel.Error, 0, $"AccountRepository-IsValidUser, TZ:{loginModel.UserName}, Exception: {ex.ToString()}");
                throw;
            }
        }
        public async Task<bool> IsValidUser(Login loginModel)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = @"SELECT * FROM Acount WHERE UserName = @UserName AND PasswordHash = @PasswordHash";
                    conn.Open();
                    var result = await conn.QueryFirstOrDefaultAsync(sQuery, new { loginModel.UserName, PasswordHash = loginModel.Password });
                    bool isValidUser = (object)result != null;
                    return isValidUser;
                }
            }
            catch (Exception ex)
            {
                //siteLogger.InsertAsync(LogLevel.Error, 0, $"AccountRepository-IsValidUser, TZ:{loginModel.UserName}, Exception: {ex.ToString()}");
                throw;
            }
        }

        public async Task<bool> IsValidUserAsync(Login loginModel)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = @"SELECT * FROM Acount WHERE UserName = @UserName AND PasswordHash = @Password";
                    conn.Open();
                    var result = await conn.QueryFirstOrDefaultAsync(sQuery, new { UserName = loginModel.UserName, loginModel.Password });
                    bool isValidUserAsync = (object)result != null;

                    return isValidUserAsync;
                }
            }
            catch (Exception ex)
            {
                //siteLogger.InsertAsync(LogLevel.Error, 0, $"AccountRepository-IsValidUserAsync, TZ:{loginModel.UserName}, Exception: {ex.ToString()}");
                throw;
            }
        }


    }
}
