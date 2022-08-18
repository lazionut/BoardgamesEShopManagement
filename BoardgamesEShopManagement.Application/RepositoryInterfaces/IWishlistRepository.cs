using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.RepositoryInterfaces
{
    public interface IWishlistRepository
    {
        void CreateWishlist(Wishlist wishlist);
        IEnumerable<Wishlist> GetWishlists();
        Wishlist GetWishlist(int wishlistId);
        bool DeleteWishlist(int wishlistId);
    }
}
