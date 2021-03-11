﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assignment.Data.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Authentication;
using Assignment.Services.Constants;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Assignment.Data.Repository.Interface;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IAcountService _acountService;
        private readonly IConnectionsRepository _connectionsRepository;
        private readonly IConfiguration _configuration;
        private const int UserCookieExpireTimeDays = 365;


        public AccountController(IMemberService memberService, IAcountService acountService, IConfiguration configuration, IConnectionsRepository connectionsRepository)
        {
            this._configuration = configuration;
            this._memberService = memberService;
            this._acountService = acountService;
            this._connectionsRepository = connectionsRepository;
        }
        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<ActionResult> Login(Login login)
        {
            try
            {
                var loginResult = new LoginResultModel();
                var member = await _memberService.GetMember(login);
                if (member == null)
                {
                    loginResult.Error = "שם משתמש או סיסמא לא תקינים";

                    return Ok(loginResult);
                }
                await SignInUser(login, member);
                loginResult.Member = member;
                loginResult.IsUserAuth = true;

                return Ok(loginResult);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("Register")]
        public async Task<ActionResult> Register([FromBody] Register registerDetails)
        {
            Login login = null;
            bool isRegister = false;
            try
            {
                isRegister = await _acountService.Register(registerDetails);
                if (isRegister)
                {
                    login = new Login()
                    {
                        Password = registerDetails.VerificationPassword,
                        UserName = registerDetails.UserName
                    };
                    return await this.Login(login);
                }
            }
            catch (Exception e)
            {

            }
            return Ok(false);
        }


        [HttpPost, Route("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(AccountConst.AppCookie);
            return Ok("user logout");
        }

        private async Task SignInUser(Login loginModel, Member member)
        {
            int userCookieExpireTimeMinutes = 20;
            int.TryParse(_configuration["AccountSettings:UserCookieExpireTimeMinutes"], out userCookieExpireTimeMinutes);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.UserName),
                //new Claim("MI", member..ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, AccountConst.AppCookie);

            var authenticationProperties = new AuthenticationProperties
            {
                ExpiresUtc = loginModel.RememberMe ? DateTimeOffset.UtcNow.AddHours(2).AddDays(UserCookieExpireTimeDays) : DateTimeOffset.UtcNow.AddHours(2).AddMinutes(userCookieExpireTimeMinutes),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(AccountConst.AppCookie, new ClaimsPrincipal(claimsIdentity), authenticationProperties);
        }
        [HttpGet, Route("GeAllLogInUsers")]
        public Task<ExtendMembers> GeAllLogInUsers()
        {
            return _connectionsRepository.GeAllLogInUsers();
        }
    }
}
