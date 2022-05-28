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
    public class OrdersRL : IOrdersRL
    {
        private readonly IConfiguration configuration;

        public OrdersRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddOrder(AddOrder addOrder, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                con.Open();
                //SqlTransaction sqlTran = con.BeginTransaction();
                SqlCommand cmd = con.CreateCommand();
                SqlTransaction sqlTran = null;
                try
                {
                    List<CartResponse> cartList = new List<CartResponse>();
                    List<string> orderList = new List<string>();

                    cmd = new SqlCommand("spGetAllCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    //cmd.Transaction = sqlTran;
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CartResponse cart = new CartResponse();
                            //cart.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                            cart.BookId = Convert.ToInt32(reader["BookId"] == DBNull.Value ? default : reader["BookId"]);
                            cartList.Add(cart);
                        }
                        reader.Close();

                        sqlTran = con.BeginTransaction();
                        foreach (var cart in cartList)
                        {
                            cmd = new SqlCommand("spAddOrders", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@AddressId", addOrder.AddressId);
                            cmd.Transaction = sqlTran;
                            int result = Convert.ToInt32(cmd.ExecuteScalar());
                            if (result != 2 && result != 3 && result != 4)
                            {
                                orderList.Add("Item Added to OrderList");
                            }
                            else
                            {
                                sqlTran.Rollback();
                                return null;
                            }
                        }
                        sqlTran.Commit();
                        con.Close();
                        return "Congratulations! Order Placed Successfully";
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    throw ex;
                }
            }
        }

        public List<OrdersResponse> GetAllOrders(int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    List<OrdersResponse> ordersResponse = new List<OrdersResponse>();
                    SqlCommand cmd = new SqlCommand("spGetAllOrders", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            OrdersResponse order = new OrdersResponse();
                            OrdersResponse temp;
                            temp = ReadData(order, rdr);
                            ordersResponse.Add(temp);
                        }
                        con.Close();
                        return ordersResponse;
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

        public OrdersResponse ReadData(OrdersResponse order, SqlDataReader rdr)
        {
            order.OrderId = Convert.ToInt32(rdr["OrderId"] == DBNull.Value ? default : rdr["OrderId"]);
            order.AddressId = Convert.ToInt32(rdr["AddressId"] == DBNull.Value ? default : rdr["AddressId"]);
            order.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            order.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
            order.BooksQty = Convert.ToInt32(rdr["BooksQty"] == DBNull.Value ? default : rdr["BooksQty"]);
            order.OrderDateTime = Convert.ToDateTime(rdr["OrderDate"] == DBNull.Value ? default : rdr["OrderDate"]);
            order.OrderDate = order.OrderDateTime.ToString("dd-MMM-yyyy");
            order.OrderPrice = Convert.ToDouble(rdr["OrderPrice"] == DBNull.Value ? default : rdr["OrderPrice"]);
            order.ActualPrice = Convert.ToDouble(rdr["ActualPrice"] == DBNull.Value ? default : rdr["ActualPrice"]);
            order.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
            order.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
            order.Author = Convert.ToString(rdr["Author"] == DBNull.Value ? default : rdr["Author"]);

            return order;
        }
    }
}
