using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Models
{
    public class Email
    {

        string HtmlBody;
        SmtpClient smtp = new SmtpClient();


        private readonly IConfiguration config;
        public Email(IConfiguration config)
        {
            this.config = config;

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("dikole.shubh@gmail.com", "Golem@411041");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public void EmailService(MSMQModel link)
        {
            try
            {
               
                MailMessage message = new MailMessage();
                message.From = new MailAddress("dikole.shubh@gmail.com");
                message.To.Add(new MailAddress(link.Email));
                message.Subject = "Reset Password";
                message.IsBodyHtml = true;
                message.Body = HtmlBody;


                smtp.Send(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}

