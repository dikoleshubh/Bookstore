using Business_Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {

        IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)                           //Constructor n passing an object to controller
        {                                                                           //to get an access of IEmployeeBL
            this.adminBL = adminBL;
        }
        [HttpPost("RegisterADmin")]                                                                  
        public IActionResult RegisterAdminDetails(RegisterAdmin registration)                                      
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Admin result = this.adminBL.RegisterAdminDetails(registration);                   
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, Message = "Admin details is added Successfully" });   
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, Message = "Admin details is added  Unsuccessfully" });
                    }
                }

                else
                {
                    throw new Exception("Model is not valid");
                }

            }


            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("ADminlogin")]
        public IActionResult LoginAdmin(LoginAdmin emailModel)
        {
            var token = this.adminBL.LoginAdmin(emailModel.AdminEmail, emailModel.AdminPassword);
            if (token == null)
                return Unauthorized();
            return this.Ok(new { token = token, success = true, message = "Token Generated Successfull" });
        }
    }
}
