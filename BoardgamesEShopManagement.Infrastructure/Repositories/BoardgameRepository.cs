using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class BoardgameRepository : GenericRepository<Boardgame>, IBoardgameRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<Boardgame> _logger;

        public BoardgameRepository(ShopContext context, ILogger<Boardgame> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Boardgame>?> GetAllSorted(int pageIndex, int pageSize, BoardgamesSortOrdersEnum sortOrder)
        {
            _logger.LogInformation("Getting the list of boardgames...");
            IQueryable<Boardgame> boardgame = _context.Boardgames;

            if (pageIndex <= 0 || pageSize <= 0 || !Enum.IsDefined(typeof(BoardgamesSortOrdersEnum), sortOrder))
            {
                return null;
            }

            switch (sortOrder)
            {
                case BoardgamesSortOrdersEnum.ReleaseYearDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by date...");
                    boardgame = boardgame.OrderByDescending(b => b.ReleaseYear);
                    break;

                case BoardgamesSortOrdersEnum.PriceAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by price...");
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;

                case BoardgamesSortOrdersEnum.PriceDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by price...");
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;

                case BoardgamesSortOrdersEnum.NameAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by name...");
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;

                case BoardgamesSortOrdersEnum.NameDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by name...");
                    boardgame = boardgame.OrderByDescending(b => b.Name);
                    break;
            }

            return await boardgame
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetAllSortedCounter()
        {
            _logger.LogInformation("Getting the number of boardgames...");
            return await _context.Boardgames.CountAsync();
        }

        public async Task<List<Boardgame>?> GetPerCategory(int categoryId, int pageIndex, int pageSize, BoardgamesSortOrdersEnum sortOrder)
        {
            _logger.LogInformation("Getting the list of boardgames per category selected by it's identifier...");
            IQueryable<Boardgame> boardgame = _context.Boardgames
                .Where(boardgame => boardgame.CategoryId == categoryId);

            if (pageIndex <= 0 || pageSize <= 0 || !Enum.IsDefined(typeof(BoardgamesSortOrdersEnum), sortOrder))
            {
                return null;
            }

            switch (sortOrder)
            {
                case BoardgamesSortOrdersEnum.ReleaseYearDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by date...");
                    boardgame = boardgame.OrderByDescending(b => b.ReleaseYear);
                    break;

                case BoardgamesSortOrdersEnum.PriceAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by price...");
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;

                case BoardgamesSortOrdersEnum.PriceDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by price...");
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;

                case BoardgamesSortOrdersEnum.NameAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by name...");
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;

                case BoardgamesSortOrdersEnum.NameDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by name...");
                    boardgame = boardgame.OrderByDescending(b => b.Name);
                    break;
            }

            return await boardgame
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetPerCategoryCounter(int categoryId)
        {
            _logger.LogInformation("Getting the number of boardgames per category selected by it's identifier...");
            return await _context.Boardgames.Where(boardgame => boardgame.CategoryId == categoryId).CountAsync();
        }

        public async Task<List<Boardgame>?> GetPerName(string characters, int pageIndex, int pageSize, BoardgamesSortOrdersEnum sortOrder)
        {
            _logger.LogInformation("Getting the list of boardgames by searched keywords...");
            IQueryable<Boardgame> boardgame = _context.Boardgames
                .Where(boardgame => boardgame.Name.Contains(characters));

            if (pageIndex <= 0 || pageSize <= 0 || !Enum.IsDefined(typeof(BoardgamesSortOrdersEnum), sortOrder))
            {
                return null;
            }

            switch (sortOrder)
            {
                case BoardgamesSortOrdersEnum.ReleaseYearDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by date...");
                    boardgame = boardgame.OrderByDescending(b => b.ReleaseYear);
                    break;

                case BoardgamesSortOrdersEnum.PriceAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by price...");
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;

                case BoardgamesSortOrdersEnum.PriceDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by price...");
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;

                case BoardgamesSortOrdersEnum.NameAscending:
                    _logger.LogInformation("Getting the list of boardgames sorted by name...");
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;

                case BoardgamesSortOrdersEnum.NameDescending:
                    _logger.LogInformation("Getting the list of boardgames sorted descending by name...");
                    boardgame = boardgame.OrderByDescending(b => b.Name);
                    break;
            }

            return await boardgame
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }

        public async Task<int> GetPerNameCounter(string characters)
        {
            _logger.LogInformation("Getting the number of boardgames by searched keywords...");
            return await _context.Boardgames.Where(boardgame => boardgame.Name.Contains(characters)).CountAsync();
        }

        public async Task<List<string>> GetNames()
        {
            _logger.LogInformation("Getting the number of boardgames by searched keywords...");
            return await _context.Boardgames.Select(boardgame => boardgame.Name).ToListAsync();
        }
    }
}