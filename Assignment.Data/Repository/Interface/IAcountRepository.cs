using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Repository.Interface
{
    public interface IAcountRepository
    {
        Task<bool> IsValidUser(Login login);
        Task<bool> IsValidUserAsync(Login loginModel);
        Task<bool> Register(Login loginModel);


    }
}
