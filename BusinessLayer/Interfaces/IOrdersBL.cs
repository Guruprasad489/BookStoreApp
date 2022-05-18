using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IOrdersBL
    {
        public AddOrder AddOrder(AddOrder addOrder, int userId);
        public List<OrdersResponse> GetAllOrders(int userId);
    }
}
