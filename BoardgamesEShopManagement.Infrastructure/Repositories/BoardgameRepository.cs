using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

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

        public async Task<List<Boardgame>> GetBoardgamesSorted(int pageIndex, int pageSize, string sortOrder)
        {
            _logger.LogInformation("Getting the list of boardgames...");
            IQueryable<Boardgame> boardgame = _context.Boardgames;

            if (pageIndex <= 0 || pageSize <= 0 || (sortOrder != "price_ascending" &&
                sortOrder != "price_descending" && sortOrder != "name_ascending" && sortOrder != "name_descending"))
            {
                return null;
            }

            switch (sortOrder)
            {
                case "price_ascending":
                    _logger.LogInformation("Getting the list of boardgames sorted by price...");
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;
                case "price_descending":
                    _logger.LogInformation("Getting the list of boardgames sorted descending by price...");
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;
                case "name_ascending":
                    _logger.LogInformation("Getting the list of boardgames sorted by name...");
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;
                case "name_descending":
                    _logger.LogInformation("Getting the list of boardgames sorted descending by name...");
                    boardgame = boardgame.OrderByDescending(b => b.Name);
                    break;
            }

            return await boardgame
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Boardgame>> GetBoardgamesPerCategory(int categoryId, int pageIndex, int pageSize, string sortOrder)
        {
            _logger.LogInformation("Getting the list of boardgames per category selected by it's identifier...");
            IQueryable<Boardgame> boardgame = _context.Boardgames
                .Where(boardgame => boardgame.CategoryId == categoryId);

            if (pageIndex <= 0 || pageSize <= 0 || (sortOrder != "price_ascending" &&
                sortOrder != "price_descending" && sortOrder != "name_ascending" && sortOrder != "name_descending"))
            {
                return null;
            }

            switch (sortOrder)
            {
                case "price_ascending":
                    _logger.LogInformation("Getting the list of boardgames sorted by price...");
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;
                case "price_descending":
                    _logger.LogInformation("Getting the list of boardgames sorted descending by price...");
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;
                case "name_ascending":
                    _logger.LogInformation("Getting the list of boardgames sorted by name...");
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;
                case "name_descending":
                    _logger.LogInformation("Getting the list of boardgames sorted descending by name...");
                    boardgame = boardgame.OrderByDescending(b => b.Name);
                    break;
            }

            return await boardgame
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Boardgame>> GetBoardgamesByName(string characters, int pageIndex, int pageSize, string sortOrder)
        {
            _logger.LogInformation("Getting the list of boardgames by searched keywords...");
            IQueryable<Boardgame> boardgame = _context.Boardgames
                .Where(boardgame => boardgame.Name.Contains(characters));

            if (pageIndex <= 0 || pageSize <= 0 || (sortOrder != "price_ascending" &&
                sortOrder != "price_descending" && sortOrder != "name_ascending" && sortOrder != "name_descending"))
            {
                return null;
            }

            switch (sortOrder)
            {
                case "price_ascending":
                    _logger.LogInformation("Getting the list of boardgames sorted by price...");
                    boardgame = boardgame.OrderBy(b => b.Price);
                    break;
                case "price_descending":
                    _logger.LogInformation("Getting the list of boardgames sorted descending by price...");
                    boardgame = boardgame.OrderByDescending(b => b.Price);
                    break;
                case "name_ascending":
                    _logger.LogInformation("Getting the list of boardgames sorted by name...");
                    boardgame = boardgame.OrderBy(b => b.Name);
                    break;
                case "name_descending":
                    _logger.LogInformation("Getting the list of boardgames sorted descending by name...");
                    boardgame = boardgame.OrderByDescending(b => b.Name);
                    break;
            }

            return await boardgame
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }
    }
}
