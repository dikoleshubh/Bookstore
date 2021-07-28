using Microsoft.Extensions.Configuration;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Service
{
    public class CartRL : ICartRL
    {
        private static string connectionString = "Data Source=.;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }

        public CartRL(IConfiguration configuration)
        {

            Configuration = configuration;
        }


        public ICollection<UserCart> AddBookToCart(long ID, long BookID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {

                        connection.Open();
                        SqlCommand cmd = new SqlCommand("AddToCart", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                       // User amd = new User();
                        //ResponseBook abc = new ResponseBook();
                        cmd.Parameters.AddWithValue("ID", ID);
                        cmd.Parameters.AddWithValue("BookID", BookID);
                        var returnParameter = cmd.Parameters.Add("result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.Output;
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;
                        if (result != null && result.Equals(3))
                        {
                            throw new Exception("Book out of stock");
                        }
                        else if (result != null && result.Equals(2))
                        {
                            throw new Exception("Book doesn't exist");
                        }
                        ICollection<UserCart> cart = new List<UserCart>();
                        UserCart Book = new UserCart();
                      
                            while (rd.Read())
                            {

                                Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt32("BookID");
                                Book.TotalCost = rd["TotalCost"] == DBNull.Value ? default : rd.GetInt32("TotalCost");
                                Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                                Book.CartID = rd["CartID"] == DBNull.Value ? default : rd.GetInt64("CartID");
                                Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                                Book.ID = rd["ID"] == DBNull.Value ? default : rd.GetInt64("ID");
                                Book.Count = rd["BookCount"] == DBNull.Value ? default : rd.GetInt32("BookCount");
                                cart.Add(Book);
                            }
                        
                        return cart;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
