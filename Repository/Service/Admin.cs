using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository.Service
{
    public class AdminRL : IAdminRL
    {
        private static string connectionString = "Data Source=.;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }
        public AdminRL(IConfiguration configuration)
        {

            Configuration = configuration;


        }

        SqlConnection connection = new SqlConnection(connectionString);


        public Admin RegisterAdminDetails(RegisterAdmin registration)
        {
            try
            {

                Admin admin = new Admin();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        connection.Open();
                        //define the SqlCommand Object
                        SqlCommand cmd = new SqlCommand("RegisterAdmin", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                       
                        cmd.Parameters.AddWithValue("@AdminEmail", registration.AdminEmail);
                        cmd.Parameters.AddWithValue("@AdminPassword", Password.ConvertToEncrypt(registration.AdminPassword));

                        SqlDataReader sqlDataReader = cmd.ExecuteReader();


                        if (sqlDataReader.HasRows)
                        {
                            if (sqlDataReader.Read())
                            {
                                admin.AdminID = sqlDataReader.GetInt64(0);
                                admin.AdminEmail = sqlDataReader.GetString(1);
                                
                                

                            }
                        }

                        return admin;

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



       /* public string LoginAdmin(string AdminEmail, string AdminPassword)
        {
            //string encodedPassword = EncryptPassword(password);
            //var result = _userDbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == encodedPassword);

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand cmd = new SqlCommand("AdminLogin", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("AdminEmail", AdminEmail);
            cmd.Parameters.AddWithValue("AdminPassword", AdminPassword);
            var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            Admin customer = new Admin();
            SqlDataReader rd = cmd.ExecuteReader();
            var result = returnParameter.Value;


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("HelloThisTokenIsGeneretedByMe");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, AdminEmail),
                   // new Claim ("ID", result.ID.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }*/





        public string AdminLogin(LoginAdmin login)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("AdminLogin", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@AdminEmail", login.AdminEmail);
                cmd.Parameters.AddWithValue("@AdminPassword", Password.ConvertToEncrypt(login.AdminPassword));
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                Admin admin = new Admin();

                if (sqlDataReader.HasRows)
                {
                    if (sqlDataReader.Read())
                    {
                        admin.AdminID = sqlDataReader.GetInt32(0);
                        admin.AdminEmail = sqlDataReader.GetString(1);
                      //  admin.PhoneNumber = sqlDataReader.GetInt64(2);
                        //admin.Email = sqlDataReader.GetString(3);

                    }
                }

                var result = returnParameter.Value;
                if (result != null && result.Equals(2))
                {
                    throw new Exception("AdminID is invalid");
                }
                if (result != null && result.Equals(3))
                {
                    throw new Exception("wrong password");
                }

                string token1 = CreateAdminToken(admin);
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

        public string CreateAdminToken(Admin info)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "Admin"),

                            new Claim("AdminEmail", info.AdminEmail.ToString() ),

                            new Claim("AdminID", info.AdminID.ToString()),

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




    }




}

