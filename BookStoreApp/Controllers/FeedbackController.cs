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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;
        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [HttpPost("Add")]
        [Authorize]
        public IActionResult AddFeedback(AddFeedback addFeedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = feedbackBL.AddFeedback(addFeedback, userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Feedback Added sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Add Feedback" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllFeedbacks(int bookId)
        {
            try
            {
                var res = feedbackBL.GetAllFeedbacks(bookId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Feedbacks Retrieved sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Retrieve Feedbacks" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
