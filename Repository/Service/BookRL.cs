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
    public class BookRL : IBookRL
    {

        private static string connectionString = "Data Source=.;Initial Catalog=BookStore;Integrated Security=SSPI";
        private IConfiguration Configuration { get; }
        ResponseBook Book;
        public BookRL(IConfiguration configuration)
        {

            Configuration = configuration;
        }
        public ResponseBook AddBook(ResponseBook book)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {

                        connection.Open();
                        SqlCommand cmd = new SqlCommand("AddBook", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("BookName", book.BookName);
                        cmd.Parameters.AddWithValue("BookDescription", book.BookDescription);
                        cmd.Parameters.AddWithValue("BookImage", book.BookImage);
                        cmd.Parameters.AddWithValue("BookPrice", book.BookPrice);
                        cmd.Parameters.AddWithValue("BookQuantity", book.Quantity);
                        cmd.Parameters.AddWithValue("AuthorName", book.AuthorName);
                        SqlDataReader rd = cmd.ExecuteReader();
                        ResponseBook Book = new ResponseBook();

                       
                            while (rd.Read())
                            {

                                Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt32("BookID");
                                Book.BookDescription = rd["BookDescription"] == DBNull.Value ? default : rd.GetString("BookDescription");
                                Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                                Book.Quantity = rd["BookQuantity"] == DBNull.Value ? default : rd.GetInt32("BookQuantity");
                                Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                                Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                                Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                                Book.InStock = rd["InStock"] != DBNull.Value && rd.GetBoolean("InStock");
                            }
                       
                        return Book;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool DeleteBook(long bookID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (connection)
                    {
                        ICollection<ResponseBook> Books = new List<ResponseBook>();

                        connection.Open();

                        SqlCommand cmd = new SqlCommand("DeleteBooks", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("BookID", bookID);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();
                        var result = returnParameter.Value;
                        if (result.Equals(2))
                        {
                            throw new Exception("book don't exist");
                        }
                        else if (result.Equals(3))
                        {
                            throw new Exception("book already deleted");
                        }
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ICollection<ResponseBook> GetUserBooks()
        {
            try
            {
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        using (connection)
                        {
                            List<ResponseBook> Books = new List<ResponseBook>();
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("GetBooks", connection)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            SqlDataReader rd = cmd.ExecuteReader();
                            while (rd.Read())
                            {
                                ResponseBook Book = new ResponseBook();
                                Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt32("BookID");
                                Book.BookDescription = rd["BookDescription"] == DBNull.Value ? default : rd.GetString("BookDescription");
                                Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                                Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                                Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                                Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                                Book.Quantity= rd["BookQuantity"]== DBNull.Value ? default : rd.GetInt32("BookQuantity");
                                //Book.InStock = rd["InStock"] == DBNull.Value ? default : rd.GetBoolean("InStock");
                                Books.Add(Book);
                            }
                            return Books;
                        }
                    }
                }
            }


            catch (Exception)
            {

                throw;
            }

        }
        public ICollection<ResponseBook> GetPriceSortBooks(long ID, bool sort)
        {
            try
            {
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        using (connection)
                        {
                            List<ResponseBook> Books = new List<ResponseBook>();
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("GetPriceSortedBooks", connection)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmd.Parameters.AddWithValue("ID", ID);
                            cmd.Parameters.AddWithValue("order", sort);
                            SqlDataReader rd = cmd.ExecuteReader();
                            while (rd.Read())
                            {
                                ResponseBook Book = new ResponseBook();
                                Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt32("BookID");
                                Book.BookDescription = rd["BookDescription"] == DBNull.Value ? default : rd.GetString("BookDescription");
                                Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                                Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                                Book.AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName");
                                Book.BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage");
                                Book.InStock = rd["InStock"] == DBNull.Value ? default : rd.GetBoolean("InStock");
                                Books.Add(Book);
                            }
                            return Books;
                        }
                    }
                }
            }


            catch (Exception)
            {

                throw;
            }

        }
        public ResponseBook UpdateBook(long BookID, ResponseBook book)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UpdateBook", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("BookID", BookID);
                        cmd.Parameters.AddWithValue("BookName", book.BookName);
                        cmd.Parameters.AddWithValue("BookDescription", book.BookDescription);
                        cmd.Parameters.AddWithValue("BookImage", "book");
                        cmd.Parameters.AddWithValue("BookPrice", book.BookPrice);
                        cmd.Parameters.AddWithValue("BookQuantity", book.Quantity);
                        cmd.Parameters.AddWithValue("AuthorName", book.AuthorName);
                        var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        SqlDataReader rd = cmd.ExecuteReader();
                        var result = returnParameter.Value;
                        if (result != null && result.Equals(2))
                            throw new Exception("book don't exist");
                        if (rd.Read())
                        {
                            Book = new ResponseBook
                            {
                                BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt32("BookID"),
                                BookDescription = rd["BookDescription"] == DBNull.Value ? default : rd.GetString("BookDescription"),
                                BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice"),
                                BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName"),
                                AuthorName = rd["AuthorName"] == DBNull.Value ? default : rd.GetString("AuthorName"),
                                BookImage = rd["BookImage"] == DBNull.Value ? default : rd.GetString("BookImage"),
                                InStock = rd["InStock"] != DBNull.Value && rd.GetBoolean("InStock")
                            };

                        }
                        return Book;

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
