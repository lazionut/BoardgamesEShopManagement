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
        private readonly ILogger<BoardgameRepository> _logger;

        public BoardgameRepository(ShopContext context, ILogger<BoardgameRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Boardgame>?> GetAllSorted(int pageIndex, int pageSize, BoardgamesSortOrdersMode sortOrder)
        {
            IQueryable<Boardgame> boardgame = _context.Boardgames;

            if (pageIndex <= 0 || pageSize <= 0 || !Enum.IsDefined(typeof(BoardgamesSortOrdersMode), sortOrder))
            {
                return null;
            }

            switch (sortOrder)
            {
                case BoardgamesSortOrdersMode.ReleaseYearDescending:
                    _logger.LogInformation("Reading all {Boardgame} sorted descending by date", typeof(Boardgame).Name);
                    boardgame = boardgame.OrderByDescending(b => b.ReleaseYear);
                    break;

                case BoardgamesSortOrdersMode.PriceAscending:
                    _logger.LogInformation("Reading all {Boardgame} sorted by price", typeof(Boardgame).Name);
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;

                case BoardgamesSortOrdersMode.PriceDescending:
                    _logger.LogInformation("Reading all {Boardgame} sorted descending by price", typeof(Boardgame).Name);
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;

                case BoardgamesSortOrdersMode.NameAscending:
                    _logger.LogInformation("Reading all {Boardgame} sorted by name", typeof(Boardgame).Name);
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;

                case BoardgamesSortOrdersMode.NameDescending:
                    _logger.LogInformation("Reading all {Boardgame} sorted descending by name", typeof(Boardgame).Name);
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
            _logger.LogInformation("Reading number of {Boardgame}", typeof(Boardgame).Name);
            return await _context.Boardgames.CountAsync();
        }

        public async Task<List<Boardgame>?> GetPerCategory(int categoryId, int pageIndex, int pageSize, BoardgamesSortOrdersMode sortOrder)
        {
            IQueryable<Boardgame> boardgame = _context.Boardgames
                .Where(boardgame => boardgame.CategoryId == categoryId);

            if (pageIndex <= 0 || pageSize <= 0 || !Enum.IsDefined(typeof(BoardgamesSortOrdersMode), sortOrder))
            {
                return null;
            }

            switch (sortOrder)
            {
                case BoardgamesSortOrdersMode.ReleaseYearDescending:
                    _logger.LogInformation("Reading all {Boardgame} with category id {Id} sorted descending by release year", typeof(Boardgame).Name, categoryId);
                    boardgame = boardgame.OrderByDescending(b => b.ReleaseYear);
                    break;

                case BoardgamesSortOrdersMode.PriceAscending:
                    _logger.LogInformation("Reading all {Boardgame} with category id {Id} sorted by price", typeof(Boardgame).Name, categoryId);
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;

                case BoardgamesSortOrdersMode.PriceDescending:
                    _logger.LogInformation("Reading all {Boardgame} with category id {Id} sorted descending by price", typeof(Boardgame).Name, categoryId);
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;

                case BoardgamesSortOrdersMode.NameAscending:
                    _logger.LogInformation("Reading all {Boardgame} with category id {Id} sorted by name", typeof(Boardgame).Name, categoryId);
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;

                case BoardgamesSortOrdersMode.NameDescending:
                    _logger.LogInformation("Reading all {Boardgame} with category id {Id} sorted descending by name", typeof(Boardgame).Name, categoryId);
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
            _logger.LogInformation("Reading number of {Boardgame} with category id {CategoryId}", typeof(Boardgame).Name, categoryId);
            return await _context.Boardgames.Where(boardgame => boardgame.CategoryId == categoryId).CountAsync();
        }

        public async Task<List<Boardgame>?> GetPerName(string characters, int pageIndex, int pageSize, BoardgamesSortOrdersMode sortOrder)
        {
            IQueryable<Boardgame> boardgame = _context.Boardgames
                .Where(boardgame => boardgame.Name.Contains(characters));

            if (pageIndex <= 0 || pageSize <= 0 || !Enum.IsDefined(typeof(BoardgamesSortOrdersMode), sortOrder))
            {
                return null;
            }

            switch (sortOrder)
            {
                case BoardgamesSortOrdersMode.ReleaseYearDescending:
                    _logger.LogInformation("Reading all {Boardgame} by searched characters sorted descending by release year", typeof(Boardgame).Name);
                    boardgame = boardgame.OrderByDescending(b => b.ReleaseYear);
                    break;

                case BoardgamesSortOrdersMode.PriceAscending:
                    _logger.LogInformation("Reading all {Boardgame} by searched characters sorted by price", typeof(Boardgame).Name);
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;

                case BoardgamesSortOrdersMode.PriceDescending:
                    _logger.LogInformation("Reading all {Boardgame} by searched characters sorted descending by price", typeof(Boardgame).Name);
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;

                case BoardgamesSortOrdersMode.NameAscending:
                    _logger.LogInformation("Reading all {Boardgame} by searched characters sorted by name", typeof(Boardgame).Name);
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;

                case BoardgamesSortOrdersMode.NameDescending:
                    _logger.LogInformation("Reading all {Boardgame} by searched characters sorted descending by name", typeof(Boardgame).Name);
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
            _logger.LogInformation("Reading number of all {Boardgame} per searched characters", typeof(Boardgame).Name);
            return await _context.Boardgames.Where(boardgame => boardgame.Name.Contains(characters)).CountAsync();
        }

        public async Task<List<string>> GetNames()
        {
            _logger.LogInformation("Reading all {Boardgame} names", typeof(Boardgame).Name);
            return await _context.Boardgames.Select(boardgame => boardgame.Name).ToListAsync();
        }
    }
}