using BoardgamesEShopManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.RepositoryInterfaces
{
    public interface IWishlistRepository
    {
        void CreateWishlist(Wishlist wishlist);
        IEnumerable<Wishlist> GetWishlists();
        Wishlist GetById(int wishlistId);
        bool DeleteWishlist(int wishlistId);
    }
}
