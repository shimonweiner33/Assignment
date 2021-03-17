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
    public class MemberRepository : BaseRepository, IMemberRepository
    {
        public MemberRepository(IConfiguration configuration) : base(configuration)
        {

        }


        public async Task<Member> GetMember(string userName)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = @"SELECT UserName, FirstName, LastName FROM Member WHERE UserName = @UserName";

                    conn.Open();
                    return (await conn.QueryAsync<Member>(sQuery, new
                    {
                        userName
                    })).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                //siteLogger.InsertAsync(LogLevel.Error, 0, $"Member-GetMember, Exception: {ex.ToString()}");
                throw;
            }
        }
    }
}
