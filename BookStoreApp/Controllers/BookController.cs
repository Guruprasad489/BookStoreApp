using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [HttpPost("Add")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult AddBook(AddBook addBook)
        {
            try
            {
                var res = bookBL.AddBook(addBook);
                if (res != null)
                {
                    return Created("", new { success = true, message = "Book Added sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Add Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult UpdateBook(BookModel updateBook)
        {
            try
            {
                var res = bookBL.UpdateBook(updateBook);
                if (res != null)
                {
                    return Ok( new { success = true, message = "Book Updated sucessfully", data = res });
                }
                else
                {
                    return BadRequest( new { success = false, message = "Faild to Update Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                var res = bookBL.DeleteBook(bookId);
                if (res.ToLower().Contains("success"))
                {
                    return Ok( new { success = true, message = "Book Deleted sucessfully", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Delete Book" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var res = bookBL.GetAllBooks();
                if (res != null)
                {
                    return Ok(new { success = true, message = "GetAll Books sucessfull", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to GetAll Books" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetBookById")]
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                var res = bookBL.GetBookById(bookId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Get Book Details sucessfull", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Get Book Details" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
