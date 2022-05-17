using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        private readonly IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        } 

        public string AddToWishList(int bookId, int userId)
        {
            try
            {
                return wishListRL.AddToWishList(bookId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RemoveFromWishList(int wishListId, int userId)
        {
            try
            {
                return wishListRL.RemoveFromWishList(wishListId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WishListResponse> GetAllWishList(int userId)
        {
            try
            {
                return wishListRL.GetAllWishList(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
