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
    public class CartRL : ICartRL
    {
        private readonly IConfiguration configuration;

        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AddToCart AddToCart(AddToCart addCart, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddToCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BooksQty", addCart.BooksQty);
                    cmd.Parameters.AddWithValue("@BookId", addCart.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {   
                        return addCart;
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

        public string RemoveFromCart(int cartId, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spRemoveFromCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId", cartId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Item Removed from cart Successfully";
                    }
                    else
                    {
                        return "Failed to Remove item from cart";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public List<CartResponse> GetAllCart(int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    List<CartResponse> cartResponses = new List<CartResponse>();
                    SqlCommand cmd = new SqlCommand("spGetAllCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            CartResponse cart = new CartResponse();
                            CartResponse temp;
                            temp = ReadData(cart, rdr);
                            cartResponses.Add(temp);
                        }
                        con.Close();
                        return cartResponses;
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

        public CartResponse ReadData(CartResponse cart, SqlDataReader rdr)
        {
            cart.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            cart.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
            cart.CartId = Convert.ToInt32(rdr["CartId"] == DBNull.Value ? default : rdr["CartId"]);
            cart.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
            cart.Author = Convert.ToString(rdr["Author"] == DBNull.Value ? default : rdr["Author"]);
            cart.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
            cart.DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"] == DBNull.Value ? default : rdr["DiscountPrice"]);
            cart.ActualPrice = Convert.ToDouble(rdr["ActualPrice"] == DBNull.Value ? default : rdr["ActualPrice"]);
            cart.BooksQty = Convert.ToInt32(rdr["BooksQty"] == DBNull.Value ? default : rdr["BooksQty"]);
            cart.Stock = Convert.ToInt32(rdr["Quantity"] == DBNull.Value ? default : rdr["Quantity"]);
            
            return cart;
        }

        public string UpdateQtyInCart(int cartId, int bookQty, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    CartResponse cartResponses = new CartResponse();
                    SqlCommand cmd = new SqlCommand("spUpdateQtyInCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId", cartId);
                    cmd.Parameters.AddWithValue("@BooksQty", bookQty);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Quantity updated in Cart successfully";
                    }
                    else
                    {
                        return "Failed to Update Quantity in Cart";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }


    }
}
