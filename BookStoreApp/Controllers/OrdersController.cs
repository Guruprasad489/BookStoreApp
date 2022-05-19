using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.User)]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersBL ordersBL;
        public OrdersController(IOrdersBL ordersBL)
        {
            this.ordersBL = ordersBL;
        }

        [HttpPost("Add")]
        public IActionResult AddOrder(AddOrder addOrder)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = ordersBL.AddOrder(addOrder, userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Order" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllOrders()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = ordersBL.GetAllOrders(userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Orders Retrieved sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Retrieve Orders" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}

