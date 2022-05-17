using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public AddToCart AddToCart(AddToCart addCart, int userId)
        {
            try
            {
                return cartRL.AddToCart(addCart, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RemoveFromCart(int cartId, int userId)
        {
            try
            {
                return cartRL.RemoveFromCart(cartId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartResponse> GetAllCart(int userId)
        {
            try
            {
                return cartRL.GetAllCart(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateQtyInCart(int cartId, int bookQty, int userId)
        {
            try
            {
                return cartRL.UpdateQtyInCart(cartId, bookQty, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
