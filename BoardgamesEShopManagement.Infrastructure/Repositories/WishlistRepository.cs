using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ShopContext _context;

        public WishlistRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task Create(int wishlistId, int boardgameId, Wishlist wishlist)
        {
            Wishlist? searchedWishlist = await _context.Wishlists
                .Include(wishlist => wishlist.Boardgames)
                .SingleOrDefaultAsync(wishlist => wishlist.Id == wishlistId);
            if (searchedWishlist == null)
                throw new GenericItemException($"{searchedWishlist} can\'t be found!");

            Boardgame? searchedBoardgame = await _context.Boardgames.SingleOrDefaultAsync(boardgame => boardgame.Id == boardgameId);
            if (searchedBoardgame == null)
                throw new GenericItemException($"{searchedBoardgame} can\'t be found!");

            wishlist.Boardgames.Add(searchedBoardgame);
        }

        public async Task<List<Wishlist>> GetWishlistsListPerAccount(int accountId)
        {
            if (accountId >= 0)
            {
                return await _context.Wishlists
                    .Include(wishlist => wishlist.Boardgames)
                    .Where(wishlist => wishlist.AccountId == accountId)
                    .ToListAsync();
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<Wishlist> GetById(int wishlistId)
        {
            if (wishlistId >= 0)
            {
                return await _context.Wishlists
                    .Include(order => order.Boardgames)
                    .SingleOrDefaultAsync(order => order.Id == wishlistId);
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<Wishlist> GetByAccount(int accountId, int wishlistId)
        {
            if (wishlistId >= 0)
            {
                return await _context.Wishlists
                    .Include(wishlist => wishlist.Boardgames)
                    .SingleOrDefaultAsync(wishlist => wishlist.AccountId == accountId && wishlist.Id == wishlistId);
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<bool> Delete(int wishlistId)
        {
            if (wishlistId >= 0)
            {
                Wishlist? searchedWishlist = await _context.Wishlists.SingleOrDefaultAsync(wishlist => wishlist.Id == wishlistId);
                return _context.Wishlists.Remove(searchedWishlist) != null ? true : false;
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
