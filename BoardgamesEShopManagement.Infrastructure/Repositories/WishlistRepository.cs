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

        public IEnumerable<Wishlist> GetWishlists()
        {
            return wishlists;
        }

        public Wishlist GetWishlistById(int wishlistId)
        {
            if (wishlistId >= 0)
            {
                return wishlists.FirstOrDefault(wishlist => wishlist.Id == wishlistId);
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public bool DeleteWishlist(int wishlistId)
        {
            if (wishlistId >= 0)
            {
                Wishlist searchedWishlist = wishlists.FirstOrDefault(wishlist => wishlist.Id == wishlistId);
                return wishlists.Remove(searchedWishlist);
            }
            else
            {
                throw new NegativeIdException();
            }
        }
    }
}
