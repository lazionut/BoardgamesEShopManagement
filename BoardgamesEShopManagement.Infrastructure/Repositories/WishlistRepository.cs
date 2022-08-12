using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        public readonly List<Wishlist> wishlists = new();

        public void CreateWishlist(Wishlist wishlist)
        {
            wishlists.Add(wishlist);
            wishlist.Id = wishlists.Count;
        }

        public Wishlist GetById(int wishlistId)
        {

            return wishlists.FirstOrDefault(wishlist => wishlist.Id == wishlistId);
        }

        public IEnumerable<Wishlist> GetWishlists()
        {
            return wishlists;
        }

        public bool DeleteWishlist(int wishlistId)
        {
            Wishlist searchedWishlist = wishlists.FirstOrDefault(wishlist => wishlist.Id == wishlistId);
            return wishlists.Remove(searchedWishlist);
        }
    }
}
