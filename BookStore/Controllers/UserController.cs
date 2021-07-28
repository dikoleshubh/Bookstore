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
      //  private readonly ILogger<UserController> _logger;
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

        [HttpPost("Login")]
        public IActionResult Login(LoginUser login)                                        //Here return type represents the result of an action method
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string result = this.UserBL.Login(login);                   //getting the data from BusinessLayer
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, Message = "Login Successfully", data = result });   //(smd format)    //this.Ok returns the data in json format
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, Message = "Login  Unsuccessful" });
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

        /*  [HttpPost("ForgetPassword")]
           public ActionResult ForgetPassword(ForgetPasswordModel forgetPasswordModel)
           {
               try
               {
                   if (ModelState.IsValid)
                   {
                       MSMQModel result = UserBL.ForgetPassword(forgetPasswordModel);                   //getting the data from BusinessLayer
                       var msmq = new MSMQ(Configuration);
                       msmq.MSMQSender(result);
                       if (result != null)
                       {
                           return this.Ok(new { Success = true, Message = "Your password has been forget sucessfully now you can reset your password" });   //(smd format)    //this.Ok returns the data in json format
                       }

                       else
                       {
                           return this.Ok(new { Success = true, Message = "Other User is trying to login from your account" });   //(smd format)    //this.Ok returns the data in json format
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
        */







        [HttpPost]
        [Route("ForgotPassword")]



        public IActionResult ForgotPassword(string Email)
        {
            try
            {
                //_logger.LogInformation("The API for Forgot Password has accessed");
                var result = this.UserBL.ForgotPassword(Email);
                if (result == true)
                {
                    // _logger.LogInformation("Link has sent to given gmail to reset password");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Link has sent to the given email address to reset the password" });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Unable to sent link to given email address. This Email doesn't exist in database." });
            }
            catch (Exception ex)
            {
                //_logger.LogWarning("Exception encountered while sending link to given mail address" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
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



       








    }




}


       
   
