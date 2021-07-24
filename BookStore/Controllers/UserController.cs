using Business_Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        IUserBL UserBL;
        private IConfiguration Configuration { get; }
        private readonly ILogger<UserController> _logger;
        public UserController(IUserBL UserBL)
        {
            this.UserBL = UserBL;
          
        }

        [HttpPost("Register")]
        public IActionResult RegisterDetails(RegisterUser registration)
        {
            try
            {
              
                if (ModelState.IsValid)
                {
                    //_logger.LogInformation("The API for User Registartion has been accessed");

                    User result = this.UserBL.RegisterDetails(registration);
                    if (result != null)
                    {
                      //  _logger.LogInformation("The API for User Registartion done");
                        return this.Ok(new { Success = true, Message = "User Registration is completed" });
                    }
                    else
                    {
                        //_logger.LogInformation("The API for User Registartion Unscuc");
                        return this.BadRequest(new { Success = false, Message = "Registration  Unsuccessfully" });
                    }
                }

                else
                {
                    throw new Exception("Failed");
                }

            }


            catch (Exception e)
            {
               // _logger.LogWarning("An Exception caugth while adding new user" + e.Message);
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }
        /*

        [HttpPost("Login")]
         public IActionResult Login(LoginUser User)
         {
             if (User == null)
             {
                 return BadRequest("Customer is null.");
             }
             try
             {
                // _logger.LogInformation("The API for User Login has been accessed");
                 User result = UserBL.Login(User);
                 if (result != null)
                 {
                  //   _logger.LogInformation("User Logged Login Successfull!");
                     return Ok(new { success = true, Message = "Customer login Successful", User = result });
                 }
                 else
                 {

                     return BadRequest(new { success = false, Message = "Customer login Unsuccessful" });
                 }
             }
             catch (Exception exception)
             {
                 return BadRequest(new { success = false, exception.Message });
             }
         }*/

        
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser(LoginUser emailModel)
        {
            var token = this.UserBL.Login(emailModel.Email, emailModel.Password);
            if (token == null)
                return Unauthorized();
            return this.Ok(new { token = token, success = true, message = "Token Generated Successfull" });
        }
        /*
        [HttpPost("ForgetPassword")]
        public ActionResult ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MSMQModel result = UserBL.ForgetPassword(forgetPasswordModel);                  
                    var msmq = new MSMQ(Configuration);
                    msmq.MSMQSender(result);
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, Message = "Your password has been forget sucessfully now you can reset your password" });  
                     }

                    else
                    {
                        return this.Ok(new { Success = true, Message = "Other User is trying to login from your account" });   
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var Email = claims.Where(p => p.Type == "Email").FirstOrDefault()?.Value;
                    resetPasswordModel.Email = Email;
                    bool result = UserBL.ResetCustomerAccountPassword(resetPasswordModel);
                    if (result)
                    {
                        return Ok(new { success = true, Message = "password changed successfully" });
                    }
                }
                return BadRequest(new { success = false, Message = "password change unsuccessfull" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        */

    }

}
       
   
