using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<Wishlist> _logger;

        public WishlistRepository(ShopContext context, ILogger<Wishlist> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(Wishlist wishlist)
        {
            _logger.LogInformation("Preparing to add wishlist to the database...");
            await _context.Wishlists.AddAsync(wishlist);
        }

        public async Task CreateItem(int accountId, int wishlistId, int boardgameId, Wishlist wishlist)
        {
            _logger.LogInformation("Finding wishlist by it's identifier...");
            Wishlist? searchedWishlist = await _context.Wishlists
                .Include(wishlist => wishlist.Boardgames)
                .SingleAsync(wishlist => wishlist.Id == wishlistId && wishlist.AccountId == accountId);

            _logger.LogInformation("Finding boardgame by it's identifier...");
            Boardgame? searchedBoardgame = await _context.Boardgames
                .SingleAsync(boardgame => boardgame.Id == boardgameId);

            _logger.LogInformation("Preparing to add the boardgame in the wishlist...");
            wishlist.Boardgames.Add(searchedBoardgame);
        }

        public async Task<List<Wishlist>> GetPerAccount(int accountId)
        {
            _logger.LogInformation("Getting the list of wishlists by an account identifier...");
            return await _context.Wishlists
                .Include(wishlist => wishlist.Boardgames)
                .Where(wishlist => wishlist.AccountId == accountId)
                .OrderByDescending(wishlist => wishlist.Id)
                .ToListAsync();
        }

        public async Task<Wishlist?> GetById(int wishlistId)
        {
            _logger.LogInformation("Trying to get the wishlist by it's identifier...");
            return await _context.Wishlists
                .Include(order => order.Boardgames)
                .SingleOrDefaultAsync(order => order.Id == wishlistId);
        }

        public async Task<Wishlist?> GetByAccount(int accountId, int wishlistId)
        {
            _logger.LogInformation("Trying to get the wishlist by an account and it's identifier...");
            return await _context.Wishlists
                .Include(wishlist => wishlist.Boardgames)
                .SingleOrDefaultAsync(wishlist => wishlist.AccountId == accountId && wishlist.Id == wishlistId);
        }

        public async Task<Wishlist?> Delete(int accountId, int wishlistId)
        {
            _logger.LogInformation("Trying to get the wishlist by it's identifier...");
            Wishlist? searchedWishlist = await _context.Wishlists
                .SingleOrDefaultAsync(wishlist => wishlist.Id == wishlistId && wishlist.AccountId == accountId);

            if (searchedWishlist == null)
            {
                _logger.LogError("Could not find the wishlist.");
                return null;
            }

            _logger.LogInformation("Preparing to remove wishlist from the database...");
            _context.Wishlists.Remove(searchedWishlist);

            return searchedWishlist;
        }

        public async Task Save()
        {
            _logger.LogInformation("Saving current changes to the database...");
            await _context.SaveChangesAsync();
        }
    }
}
