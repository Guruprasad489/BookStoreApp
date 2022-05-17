using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRL
    {
        public string AddToWishList(int bookId, int userId);
        public string RemoveFromWishList(int wishListId, int userId);
        public List<WishListResponse> GetAllWishList(int userId);
    }
}
