using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrdersBL : IOrdersBL
    {
        private readonly IOrdersRL orderRL;
        public OrdersBL(IOrdersRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public AddOrder AddOrder(AddOrder addOrder, int userId)
        {
            try
            {
                return orderRL.AddOrder(addOrder, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdersResponse> GetAllOrders(int userId)
        {
            try
            {
                return orderRL.GetAllOrders(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
