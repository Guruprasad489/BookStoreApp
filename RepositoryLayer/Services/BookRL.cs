using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly IConfiguration configuration;

        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookModel AddBook(AddBook addBook)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName", addBook.BookName);
                    cmd.Parameters.AddWithValue("@Author", addBook.Author);
                    cmd.Parameters.AddWithValue("@BookImage", addBook.BookImage);
                    cmd.Parameters.AddWithValue("@BookDetail", addBook.BookDetail);
                    cmd.Parameters.AddWithValue("@DiscountPrice", addBook.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", addBook.ActualPrice);
                    cmd.Parameters.AddWithValue("@Quantity", addBook.Quantity);
                    cmd.Parameters.AddWithValue("@Rating", addBook.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", addBook.RatingCount);
                    cmd.Parameters.Add("@BookId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    int bookId = Convert.ToInt32(cmd.Parameters["@BookId"].Value.ToString());
                    con.Close();

                    if (result != 0)
                    {
                        BookModel bookModel = new BookModel
                        {
                            BookId = bookId,
                            BookName = addBook.BookName,
                            Author = addBook.Author,
                            BookImage = addBook.BookImage,
                            BookDetail = addBook.BookDetail,
                            DiscountPrice = addBook.DiscountPrice,
                            ActualPrice = addBook.ActualPrice,
                            Quantity = addBook.Quantity,
                            Rating = addBook.Rating,
                            RatingCount = addBook.RatingCount
                        };
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        public BookModel UpdateBook(BookModel updateBook)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUpdateBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", updateBook.BookId);
                    cmd.Parameters.AddWithValue("@BookName", updateBook.BookName);
                    cmd.Parameters.AddWithValue("@Author", updateBook.Author);
                    cmd.Parameters.AddWithValue("@BookImage", updateBook.BookImage);
                    cmd.Parameters.AddWithValue("@BookDetail", updateBook.BookDetail);
                    cmd.Parameters.AddWithValue("@DiscountPrice", updateBook.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", updateBook.ActualPrice);
                    cmd.Parameters.AddWithValue("@Quantity", updateBook.Quantity);
                    cmd.Parameters.AddWithValue("@Rating", updateBook.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", updateBook.RatingCount);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return updateBook;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        public string DeleteBook(int bookId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spDeleteBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Book Deleted Successfully";
                    }
                    else
                    {
                        return "Failed to Delete the Book";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        public List<BookModel> GetAllBooks()
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    List<BookModel> bookModel = new List<BookModel>();
                    SqlCommand cmd = new SqlCommand("spGetAllBooks", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            BookModel book = new BookModel();
                            BookModel temp;
                            temp = ReadData(book, rdr);
                            bookModel.Add(temp);
                        }
                        con.Close();
                        return bookModel;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        public BookModel GetBookById(int bookId)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
                {
                    BookModel bookModel = new BookModel();
                    SqlCommand cmd = new SqlCommand("spGetBookById", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            bookModel = ReadData(bookModel, rdr);
                        }
                        con.Close();
                        return bookModel;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookModel ReadData(BookModel bookModel, SqlDataReader rdr)
        {
            bookModel.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            bookModel.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
            bookModel.Author = Convert.ToString(rdr["Author"] == DBNull.Value ? default : rdr["Author"]);
            bookModel.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
            bookModel.BookDetail = Convert.ToString(rdr["BookDetail"] == DBNull.Value ? default : rdr["BookDetail"]);
            bookModel.DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"] == DBNull.Value ? default : rdr["DiscountPrice"]);
            bookModel.ActualPrice = Convert.ToDouble(rdr["ActualPrice"] == DBNull.Value ? default : rdr["ActualPrice"]);
            bookModel.Quantity = Convert.ToInt32(rdr["Quantity"] == DBNull.Value ? default : rdr["Quantity"]);
            bookModel.Rating = Convert.ToDouble(rdr["Rating"] == DBNull.Value ? default : rdr["Rating"]);
            bookModel.RatingCount = Convert.ToInt32(rdr["RatingCount"] == DBNull.Value ? default : rdr["RatingCount"]);

            return bookModel;
        }
    }
}
