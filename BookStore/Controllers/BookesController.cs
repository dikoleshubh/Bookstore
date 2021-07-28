using Business_Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookesController : ControllerBase
    {

     


        private readonly IBookBL bookManagementBL;
        public BookesController(IBookBL bookManagementBL)
        {
            this.bookManagementBL = bookManagementBL;
        }




        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult AddBook([FromForm] RequestBook Book)
        {
            if (Book == null)
            {
                return BadRequest("Book is null.");
            }
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    ResponseBook book = bookManagementBL.AddBook(Book);
                    if (book != null)
                    {
                        return Ok(new { success = true, Message = "Book added", book });
                    }
                }
                return BadRequest(new { success = false, Message = "Book Added Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

      
        [HttpGet("ViewBooks_User")]

        public IActionResult GetBooks()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long ID = Convert.ToInt64(claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value);

                    ICollection<ResponseBook> books = bookManagementBL.GetUserBooks();
                    if (books != null)
                    {
                        return Ok(new { success = true, Message = "books fetched", books });
                    }
                }
                return BadRequest(new { success = false, Message = "books fetch Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }


        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteBooks_Admin")]
        public IActionResult DeleteBook(long BookID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    bool result = bookManagementBL.DeleteBook(BookID);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "book deleted" });
                    }
                }
                return BadRequest(new { success = false, Message = "book delete Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateBook_Admin")]
        public IActionResult UpdateBook(long BookID, ResponseBook Book)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    ResponseBook book = bookManagementBL.UpdateBook(BookID, Book);
                    if (book != null)
                    {
                        return Ok(new { success = true, Message = "book updated", book });
                    }
                }
                return BadRequest(new { success = false, Message = "book update Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }















    }
}
