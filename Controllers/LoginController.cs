using MemRegPortal.DataAccessLayer;
using MemRegPortal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationDL _authDL;   //dependency injection
        public LoginController(IAuthenticationDL authDL)
        {
            _authDL = authDL;       //whatever methods in interface will come under authDL
        }

        [AllowAnonymous]             //no authentication
        [HttpPost]
        public async Task<IActionResult> Register(UserRequest user)
        {
            UserResponse res = new UserResponse();
            try
            {
                res = await _authDL.Register(user);
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest user)
        {
            LoginResponse res = new LoginResponse();
            try
            {
                res = await _authDL.Login(user);
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);
        }
    }
}
