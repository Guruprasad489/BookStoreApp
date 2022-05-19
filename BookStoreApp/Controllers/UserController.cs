using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Post request for register.
        /// Registers the specified user reg.
        /// </summary>
        /// <param name="userReg">The user reg.</param>
        /// <returns></returns>
        [HttpPost("Register")]
        public IActionResult Register(UserReg userReg)
        {
            try
            {
                var res = userBL.Register(userReg);
                if (res != null)
                {
                    return Created("", new { success = true,message= "User Registration sucessfull", data = res });
                    //return Ok(new { success = true, message = "Registration successfull", data = res });
                }
                else
                {
                    return BadRequest (new { success = false, message = "Faild to Register" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Post request for login
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var res = userBL.UserLogin(userLogin);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Logged in successfully", data= res});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Post request for Forgot password.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <returns></returns>
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            try
            {
                var res = userBL.ForgotPassword(forgotPassword);
                if (res.ToLower().Contains("success"))
                {
                    return Ok(new { success = true, message = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = res });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Put request for Reset password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns></returns>
        [HttpPatch("ResetPassword")]
        [Authorize(Roles = Roles.User)]
        public IActionResult ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var emailId = User.FindFirst(ClaimTypes.Email).Value;
                var res = userBL.ResetPassword(resetPassword, emailId);
                
                if (res.ToLower().Contains("success"))
                {
                    return Ok(new { success = true, message = res, });
                }
                else 
                //if (res.ToLower().Contains("match"))
                //{
                //    return BadRequest(new { success = true, message = res, });
                //}
                {
                    return BadRequest(new { success = true, message = res, });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
