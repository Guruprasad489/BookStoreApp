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

        public AddOrder AddOrder(AddOrder addOrder, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddOrders", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", addOrder.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@AddressId", addOrder.AddressId);

                    con.Open();
                    var result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();

                    if (result != 2 && result != 3 && result != 4)
                    {
                        return addOrder;
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
            order.OrderDate = order.OrderDateTime.ToString("dd-MM-yyyy");
            order.OrderPrice = Convert.ToInt32(rdr["OrderPrice"] == DBNull.Value ? default : rdr["OrderPrice"]);
            order.ActualPrice = Convert.ToInt32(rdr["ActualPrice"] == DBNull.Value ? default : rdr["ActualPrice"]);
            order.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
            order.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
            order.Author = Convert.ToString(rdr["Author"] == DBNull.Value ? default : rdr["Author"]);

            return order;
        }
    }
}
