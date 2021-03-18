using System;
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
using Assignment.Services.Connections;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IAcountService _acountService;
        private readonly IConnectionsService _connectionsService;
        private readonly IConfiguration _configuration;
        private const int UserCookieExpireTimeDays = 365;


        public AccountController(IMemberService memberService, IAcountService acountService, IConfiguration configuration,
            IConnectionsService connectionsService)
        {
            this._configuration = configuration;
            this._memberService = memberService;
            this._acountService = acountService;
            this._connectionsService = connectionsService;
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
                loginResult.Member = new ExtendMember()
                {
                    UserName = member.UserName,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    PhoneArea = member.PhoneArea,
                    PhoneNumber = member.PhoneNumber
                };
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
    string userName = User.Identity.Name;
    await HttpContext.SignOutAsync(AccountConst.AppCookie);
    return Ok(userName);
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
[HttpGet, Route("GetCurrentUser")]
public async Task<LoginResultModel> GetCurrentUser()
{
    ExtendMember extendMember = null;
    LoginResultModel loginResult = null;
    try
    {
        string userName = User.Identity.Name;
        if (userName != null)
        {
            extendMember = await _connectionsService.GetUserConnection(userName);
        }

        loginResult = new LoginResultModel();

        loginResult.Member = extendMember;
        loginResult.IsUserAuth = true;
    }
    catch (Exception e)
    {
        //log
    }
    return loginResult;
}
[HttpGet, Route("GetAllLogInUsers")]
public Task<ExtendMembers> GetAllLogInUsers()
{
    return _connectionsService.GetAllLogInUsers();
}
    }
}
