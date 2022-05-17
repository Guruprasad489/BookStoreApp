using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }

        [HttpPost("Add")]
        public IActionResult AddToWishList(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = wishListBL.AddToWishList(bookId, userId);
                if (res.ToLower().Contains("success"))
                {
                    return Ok(new { success = true, message = "Added to WishList sucessfully" });
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
        public IActionResult RemoveFromWishList(int wishListId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = wishListBL.RemoveFromWishList(wishListId, userId);
                if (res.ToLower().Contains("success"))
                {
                    return Ok(new { success = true, message = "Removed from WishList sucessfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Remove from WishList" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllWishList()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = wishListBL.GetAllWishList(userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "GetALl WishList sucessfull", data = res});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to GetAll WishList" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
