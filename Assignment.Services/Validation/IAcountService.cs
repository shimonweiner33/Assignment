using System;
using System.Threading.Tasks;
using Assignment.Data.Models;

namespace Assignment.Services
{
    public interface IAcountService
    {
        Task<bool> IsValidUser(Login login);
        Task<bool> Register(Login login);
    }
}
