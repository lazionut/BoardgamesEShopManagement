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
    public class BoardgameRepository : GenericRepository<Boardgame>, IBoardgameRepository
    {
        private readonly ShopContext _context;

        public BoardgameRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Boardgame>> GetBoardgamesPerCategory(int categoryId)
        {

            return await _context.Boardgames
                .Where(boardgame => boardgame.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<List<Boardgame>> GetBoardgamesByName(string characters)
        {
            return await _context.Boardgames
               .Where(boardgame => boardgame.Name.Contains(characters))
               .ToListAsync();
        }

        public void WriteBoardgamesNames(string filePath, List<Boardgame> boardgamesList)
        {
            using (StreamWriter boardgameStreamWriter = new StreamWriter(filePath))
            {
                foreach (Boardgame boardgame in boardgamesList)
                {
                    boardgameStreamWriter.WriteLine(boardgame.Name);
                }
            }
        }
    }
}
