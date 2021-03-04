using Assignment.Data.Models;
using System;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public interface IMemberService
    {
        Task<Member> GetMember(Login login);
    }
}
