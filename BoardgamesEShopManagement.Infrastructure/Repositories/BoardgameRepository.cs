using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
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

        public async Task<List<Boardgame>> GetBoardgamesPerCategory(int categoryId)
        {
            _logger.LogInformation("Getting the list of boardgames per category selected by it's identifier...");
            return await _context.Boardgames
                .Where(boardgame => boardgame.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<List<Boardgame>> GetBoardgamesByName(string characters)
        {
            _logger.LogInformation("Getting the list of boardgames by searched keywords...");
            return await _context.Boardgames
               .Where(boardgame => boardgame.Name.Contains(characters))
               .ToListAsync();
        }
    }
}
