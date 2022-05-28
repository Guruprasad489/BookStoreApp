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
    public class WishListRL : IWishListRL
    {
        private readonly IConfiguration configuration;

        public WishListRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddToWishList(int bookId, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddToWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {
                        return "Added to WishList Successfully";
                    }
                    else
                    {
                        return "Failed to Add to WishList";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        public string RemoveFromWishList(int wishListId, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spRemoveFromWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@WishListId", wishListId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Item Removed from WishList Successfully";
                    }
                    else
                    {
                        return "Failed to Remove item from WishList";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public List<WishListResponse> GetAllWishList(int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    List<WishListResponse> wishListResponse = new List<WishListResponse>();
                    SqlCommand cmd = new SqlCommand("spGetAllWishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            WishListResponse wishList = new WishListResponse();
                            WishListResponse temp;
                            temp = ReadData(wishList, rdr);
                            wishListResponse.Add(temp);
                        }
                        con.Close();
                        return wishListResponse;
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

        public WishListResponse ReadData(WishListResponse wishList, SqlDataReader rdr)
        {
            wishList.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            wishList.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
            wishList.WishListId = Convert.ToInt32(rdr["WishListId"] == DBNull.Value ? default : rdr["WishListId"]);
            wishList.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
            wishList.Author = Convert.ToString(rdr["Author"] == DBNull.Value ? default : rdr["Author"]);
            wishList.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
            wishList.DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"] == DBNull.Value ? default : rdr["DiscountPrice"]);
            wishList.ActualPrice = Convert.ToDouble(rdr["ActualPrice"] == DBNull.Value ? default : rdr["ActualPrice"]);

            return wishList;
        }

    }
}
