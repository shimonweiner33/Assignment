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

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMemberService memberService;
        private readonly IAcountService acountService;
        private readonly IConfiguration configuration;
        private const int UserCookieExpireTimeDays = 365;


        public AccountController(IMemberService memberService, IAcountService acountService, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.memberService = memberService;
            this.acountService = acountService;
        }
        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<ActionResult> Login(Login login)
        {
            try
            {
                var loginResult = new LoginResultModel();
                var member = await memberService.GetMember(login);
                if (member == null)
                {
                    loginResult.Error = "שם משתמש או סיסמא לא תקינים";

                    return Ok(loginResult);
                }
                await SignInUser(login, member);
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
        public async void Register(Login login)
        {
            var member = await acountService.Register(login);
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
            int.TryParse(configuration["AccountSettings:UserCookieExpireTimeMinutes"], out userCookieExpireTimeMinutes);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.UserName),
                //new Claim("MI", member..ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, AccountConst.AppCookie);

            var authenticationProperties = new AuthenticationProperties
            {
                ExpiresUtc = loginModel.RememberMe ? DateTimeOffset.UtcNow.AddDays(UserCookieExpireTimeDays) : DateTimeOffset.UtcNow.AddMinutes(userCookieExpireTimeMinutes),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(AccountConst.AppCookie, new ClaimsPrincipal(claimsIdentity), authenticationProperties);
        }
    }
}
