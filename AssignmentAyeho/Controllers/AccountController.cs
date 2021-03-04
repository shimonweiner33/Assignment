using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assignment.Data.Models;
using Assignment.Services;
namespace Assignment.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IMemberService memberService;
        IAcountService acountService;


        public AccountController(IMemberService memberService, IAcountService acountService)
        {
            this.memberService = memberService;
            this.acountService = acountService;
        }
        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async void Login(Login login)
        {
            var member = await memberService.GetMember(login);
        }

        [AllowAnonymous]
        [HttpPost, Route("Register")]
        public async void Register(Login login)
        {
            var member = await acountService.Register(login);
        }
    }
}
