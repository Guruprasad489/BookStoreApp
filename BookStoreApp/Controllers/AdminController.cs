using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin(AdminLogin adminLogin)
        {
            try
            {
                var res = adminBL.AdminLogin(adminLogin);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Logged in successfully", data = res });
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
    }
}
