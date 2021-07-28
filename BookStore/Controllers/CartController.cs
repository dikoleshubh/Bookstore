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
    public class CartController : ControllerBase

    {
        readonly ICartBL userCartBL;

        public CartController(ICartBL userCartBL)
        {
            this.userCartBL = userCartBL;
        }
       
        [Authorize(Roles = Role.User)]
        [HttpPost("AddToCart")]
        public IActionResult AddBookToCart(long BookID)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    long ID = Convert.ToInt64(claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value);
                    ICollection<UserCart> cart = userCartBL.AddBookToCart(ID, BookID);
                    if (cart != null)
                    {
                        return Ok(new { success = true, Message = "book added to cart", cart });
                    }
                }
                return BadRequest(new { success = false, Message = "book add to cart Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        

        
    }






}

