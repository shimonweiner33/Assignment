﻿using System;
using System.Threading.Tasks;
using Assignment.Common.Extensions;
using Assignment.Data.Models;
using Assignment.Data.Repository.Interface;

namespace Assignment.Services
{
    public class AcountService : IAcountService
    {

        private IAcountRepository acountRepository;

        public AcountService(IAcountRepository acountRepository)
        {
            this.acountRepository = acountRepository;
        }

        public async Task<bool> Register(Register registerDetails)
        {
            //isValid
            registerDetails.Password = registerDetails.Password.ToHashHMACSHA1();
            return await acountRepository.Register(registerDetails);
        }

        public async Task<bool> IsValidUser(Login login)
        {
            login.Password = login.Password.ToHashHMACSHA1();
            return await acountRepository.IsValidUser(login);
        }
    }
}
