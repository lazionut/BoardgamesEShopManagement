using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Exceptions;
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
            if (categoryId >= 0)
            {
                return await _context.Boardgames
                    .Where(boardgame => boardgame.CategoryId == categoryId)
                    .ToListAsync();
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<List<Boardgame>> GetBoardgamesByName(string characters)
        {
                return await _context.Boardgames
                .Where(boardgame => boardgame.Name.Contains(characters))
                .ToListAsync();
        }

        public async Task<decimal> GetBoardgamePrice(int boardgameId)
        {
            if (boardgameId >= 0)
            {
                Boardgame searchedBoardgame = await _context.Boardgames
                    .SingleOrDefaultAsync(boardgame => boardgame.Id == boardgameId);

                return searchedBoardgame.Price;
            }
            else
            {
                throw new NegativeIdException();
            }
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
