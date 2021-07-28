using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repository.NewFolder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace Repository.Service
{
    public class UserRL : IUserRL
    {

        private static string connectionString = "Data Source=.;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }
        public UserRL(IConfiguration configuration)
        {

            Configuration = configuration;


        }

        SqlConnection connection = new SqlConnection(connectionString);
        public User RegisterDetails(RegisterUser registration)
        {
            try
            {

                User customerUser = new User();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        //define the SqlCommand Object
                        SqlCommand cmd = new SqlCommand("spRegisterCustomerDetails", connection);
                        cmd.CommandType = CommandType.StoredProcedure;




                        cmd.Parameters.AddWithValue("@FirstName", registration.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", registration.LastName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", registration.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Email", registration.Email);
                        cmd.Parameters.AddWithValue("@Password", Password.ConvertToEncrypt(registration.Password));

                        SqlDataReader sqlDataReader = cmd.ExecuteReader();


                        if (sqlDataReader.HasRows)
                        {
                            if (sqlDataReader.Read())
                            {
                                customerUser.ID = sqlDataReader.GetInt32(0);
                                customerUser.FirstName = sqlDataReader.GetString(1);
                                customerUser.LastName = sqlDataReader.GetString(2);
                                customerUser.Email = sqlDataReader.GetString(3);
                               customerUser.PhoneNumber = sqlDataReader.GetInt64(4);
                            }
                        }

                        return customerUser;

                        //Close Data Reader
                        sqlDataReader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string Login(LoginUser login)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand("LoginMode", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", login.Email);
                cmd.Parameters.AddWithValue("@Password", Password.ConvertToEncrypt(login.Password));
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                User customerUser = new User();
                if (sqlDataReader.HasRows)
                {
                    if (sqlDataReader.Read())
                    {
                        customerUser.ID = sqlDataReader.GetInt32(0);
                        customerUser.FirstName = sqlDataReader.GetString(1);
                        customerUser.LastName = sqlDataReader.GetString(2);
                        customerUser.Email = sqlDataReader.GetString(4);
                      //  customerUser.PhoneNumber = sqlDataReader.GetInt64(4);
                    }
                }

                var result = returnParameter.Value;
                if (result != null && result.Equals(2))
                {
                    throw new Exception("Email-ID is invalid");
                }
                if (result != null && result.Equals(3))
                {
                    throw new Exception("wrong password");
                }

                string token1 = CreateToken(customerUser);
                return token1;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public string CreateToken(User info)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "User"),

                            new Claim("Email", info.Email.ToString() ),

                            new Claim("ID", info.ID.ToString()),

                          //  new Claim("Number", info.PhoneNumber.ToString()),

                        };

                var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                  Configuration["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public MSMQModel ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                User customerUser = new User();
                LoginAdmin forget1 = new LoginAdmin();
                JwtModel forget2 = new JwtModel();

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool ResetCustomerAccountPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("PasswordReset", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("UserEmail", resetPasswordModel.Email);
                        cmd.Parameters.AddWithValue("NewPassword", Password.ConvertToEncrypt(resetPasswordModel.NewPassword));
                        var returnParameter = cmd.Parameters.Add("@result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        User customer = new User();
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;

                        if (result != null && result.Equals(1))
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }








        public bool ForgotPassword(string Email)
        {
            try
            {
               // string password = string.Empty;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand("ForgotPass", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", Email);
                //cmd.Parameters.AddWithValue("@Password", Password.ConvertToEncrypt(login.Password));
                var returnParameter = cmd.Parameters.Add("@result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                User customerUser = new User();


                /*     if (sqlDataReader.Read())
                     {

                         customerUser.Email = sqlDataReader.GetString(4);
                         //  customerUser.PhoneNumber = sqlDataReader.GetInt64(4);
                     }*/


                var result = returnParameter.Value;
               
                  
                

                bool resultms;
                string user;
                string mailSubject = "Link to reset your BookStore App Credentials";


                if ( result.Equals(1))
                {
                    Sender sender = new Sender();
                    sender.SendMessage();
                    Receiver receiver = new Receiver();
                    var messageBody = receiver.receiverMessage();
                    user = messageBody;
                    using (MailMessage mailMessage = new MailMessage("dikole.shubh@gmail.com", Email))
                    {
                        mailMessage.Subject = mailSubject;
                        mailMessage.Body = user;
                        mailMessage.IsBodyHtml = true;
                        SmtpClient Smtp = new SmtpClient();
                        Smtp.Host = "smtp.gmail.com";
                        Smtp.EnableSsl = true;
                        Smtp.UseDefaultCredentials = false;
                        Smtp.Credentials = new NetworkCredential("dikole.shubh@gmail.com", "Golem@411041");
                        Smtp.Port = 587;
                        Smtp.Send(mailMessage);
                    }

                    resultms = true;
                    return resultms;
                }

                resultms = false;
                return resultms;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }

















}







