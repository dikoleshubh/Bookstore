using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {




        /*







                private readonly IAddressBL CustomerAddressBL;

                public AddressController(IAddressBL UserAddressBL)
                {
                    this.UserAddressBL = UserAddressBL;
                }

                [HttpPost]
                public IActionResult AddCustomerAddress(UserAddress address)
                {
                    if (address == null)
                    {
                        return BadRequest("address is null.");
                    }
                    try
                    {
                        var identity = User.Identity as ClaimsIdentity;
                        if (identity != null)
                        {
                            IEnumerable<Claim> claims = identity.Claims;
                            long ID = Convert.ToInt64(claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value);
                            address.ID = ID;
                            UserAddressResponse Address = CustomerAddressBL.AddCustomerAddress(address);
                            if (Address != null)
                            {
                                return Ok(new { success = true, Message = "Customer address added", Address });
                            }

                        }
                        return BadRequest(new { success = false, Message = "Customer address adding Unsuccessful" });
                    }
                    catch (Exception exception)
                    {
                        return BadRequest(new { success = false, exception.Message });
                    }
                }
            }*/
    }
}
