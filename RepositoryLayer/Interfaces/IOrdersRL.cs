using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrdersRL
    {
        public AddOrder AddOrder(AddOrder addOrder, int userId);
        public List<OrdersResponse> GetAllOrders(int userId);
    }
}
