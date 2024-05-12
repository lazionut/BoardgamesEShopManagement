using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<WishlistRepository> _logger;

        public WishlistRepository(ShopContext context, ILogger<WishlistRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(Wishlist wishlist)
        {
            _logger.LogInformation("Creating {Wishlist}", typeof(Wishlist).Name);
            await _context.Wishlists.AddAsync(wishlist);
        }

        public async Task CreateItem(int boardgameId, Wishlist wishlist)
        {
            _logger.LogInformation("Reading {Boardgame} with id {Id}", typeof(Boardgame).Name, boardgameId);
            Boardgame? searchedBoardgame = await _context.Boardgames
                .SingleAsync(boardgame => boardgame.Id == boardgameId);

            _logger.LogInformation("Creating {Boardgame} with id {BoardgameId} in {Wishlist} with id {WishlistId}", typeof(Boardgame).Name, boardgameId, typeof(Wishlist).Name, wishlist.Id);
            wishlist.Boardgames.Add(searchedBoardgame);
        }

        public async Task<List<Wishlist>> GetPerAccount(int accountId)
        {
            _logger.LogInformation("Reading all {Wishlist} with account id {AccountId}", typeof(Wishlist).Name, accountId);
            return await _context.Wishlists
                .Include(wishlist => wishlist.Boardgames)
                .Where(wishlist => wishlist.AccountId == accountId)
                .OrderByDescending(wishlist => wishlist.Id)
                .ToListAsync();
        }

        public async Task<Wishlist?> GetById(int wishlistId)
        {
            _logger.LogInformation("Reading {Wishlist} with id {Id}", typeof(Wishlist).Name, wishlistId);
            return await _context.Wishlists
                .Include(order => order.Boardgames)
                .SingleOrDefaultAsync(order => order.Id == wishlistId);
        }

        public async Task<Wishlist?> GetByAccount(int accountId, int wishlistId)
        {
            _logger.LogInformation("Reading {Wishlist} with account id {AccountId}", typeof(Wishlist).Name, accountId);
            return await _context.Wishlists
                .Include(wishlist => wishlist.Boardgames)
                .SingleOrDefaultAsync(wishlist => wishlist.AccountId == accountId && wishlist.Id == wishlistId);
        }

        public async Task<Wishlist?> Delete(int accountId, int wishlistId)
        {
            _logger.LogInformation("Deleting {Wishlist} with id {Id}", typeof(Wishlist).Name, wishlistId);
            Wishlist? searchedWishlist = await _context.Wishlists
                .SingleOrDefaultAsync(wishlist => wishlist.Id == wishlistId && wishlist.AccountId == accountId);

            if (searchedWishlist == null)
            {
                _logger.LogInformation("{Wishlist} with id {Id} does not exist", typeof(Wishlist).Name, wishlistId);
                return null;
            }

            _logger.LogInformation("Removing {Wishlist} with id {Id}", typeof(Wishlist).Name, wishlistId);
            _context.Wishlists.Remove(searchedWishlist);

            return searchedWishlist;
        }
    }
}