using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ShopContext _context;

        public WishlistRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task Create(Wishlist wishlist)
        {
            await _context.Wishlists.AddAsync(wishlist);
        }

        public async Task CreateItem(int wishlistId, int boardgameId, Wishlist wishlist)
        {
            Wishlist searchedWishlist = await _context.Wishlists
                .Include(wishlist => wishlist.Boardgames)
                .SingleOrDefaultAsync(wishlist => wishlist.Id == wishlistId);

            Boardgame searchedBoardgame = await _context.Boardgames
                .SingleOrDefaultAsync(boardgame => boardgame.Id == boardgameId);

            wishlist.Boardgames.Add(searchedBoardgame);
        }

        public async Task<List<Wishlist>> GetWishlistsListPerAccount(int accountId)
        {
            return await _context.Wishlists
                .Include(wishlist => wishlist.Boardgames)
                .Where(wishlist => wishlist.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<Wishlist> GetById(int wishlistId)
        {
            return await _context.Wishlists
                .Include(order => order.Boardgames)
                .SingleOrDefaultAsync(order => order.Id == wishlistId);
        }

        public async Task<Wishlist> GetByAccount(int accountId, int wishlistId)
        {
            return await _context.Wishlists
                .Include(wishlist => wishlist.Boardgames)
                .SingleOrDefaultAsync(wishlist => wishlist.AccountId == accountId && wishlist.Id == wishlistId);
        }

        public async Task<Wishlist> Delete(int wishlistId)
        {
            Wishlist searchedWishlist = await _context.Wishlists
                .SingleOrDefaultAsync(wishlist => wishlist.Id == wishlistId);

            if (searchedWishlist == null)
            {
                return null;
            }

            _context.Wishlists.Remove(searchedWishlist);

            return searchedWishlist;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
