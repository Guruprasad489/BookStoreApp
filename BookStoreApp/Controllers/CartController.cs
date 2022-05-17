using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommonLayer.Models;
using System;
using System.Linq;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost("Add")]
        public IActionResult AddToCart(AddToCart addCart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = cartBL.AddToCart(addCart, userId);
                if (res != null)
                {
                    return Ok( new { success = true, message = "Added to Cart sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Add to Cart" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("Remove")]
        public IActionResult RemoveFromCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = cartBL.RemoveFromCart(cartId, userId);
                if (res.ToLower().Contains("success"))
                {
                    return Ok(new { success = true, message = "Removed from Cart sucessfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Remove from Cart" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllCart()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = cartBL.GetAllCart(userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Get All Cart sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Get ALl Cart" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpPatch("UpdateQty")]
        public IActionResult UpdateQtyInCart(int cartId, int bookQty)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = cartBL.UpdateQtyInCart(cartId, bookQty, userId);
                if (res.ToLower().Contains("success"))
                {
                    return Ok(new { success = true, message = "Update Qty sucessfull"});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to update Qty" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
