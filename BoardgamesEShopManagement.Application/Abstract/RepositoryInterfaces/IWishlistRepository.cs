using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IWishlistRepository 
    {
        Task Create(Wishlist wishlist);
        Task CreateItem(int accountId, int wishlistId, int boardgameId, Wishlist wishlist);
        Task<List<Wishlist>> GetWishlistsListPerAccount(int accountId, int pageIndex, int pageSize);
        Task<Wishlist?> GetById(int wishlistId);
        Task<Wishlist?> GetByAccount(int accountId, int wishlistId);
        Task<Wishlist?> Delete(int accountId, int wishlistId);
        Task Save();
    }
}
