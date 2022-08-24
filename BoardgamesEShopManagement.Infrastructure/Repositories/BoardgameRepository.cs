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
                return await _context.Boardgames.Where(boardgame => boardgame.CategoryId == categoryId).ToListAsync();
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<decimal> GetBoardgamePrice(int boardgameId)
        {
            if (boardgameId >= 0)
            {
                Boardgame searchedBoardgame = await _context.Boardgames.SingleOrDefaultAsync(boardgame => boardgame.Id == boardgameId);

                return searchedBoardgame.Price;
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public async Task<Boardgame> UpdateBoardgame(int boardgameId, Boardgame boardgame)
        {
            if (boardgameId >= 0)
            {
                Boardgame searchedBoardgame = await _context.Boardgames.SingleOrDefaultAsync(boardgame => boardgame.Id == boardgameId);
                searchedBoardgame.CategoryId = boardgame.CategoryId;
                searchedBoardgame.Image = boardgame.Image ?? searchedBoardgame.Image;
                searchedBoardgame.Name = boardgame.Name ?? searchedBoardgame.Name;
                searchedBoardgame.Description = boardgame.Description ?? searchedBoardgame.Description;
                searchedBoardgame.Price = boardgame.Price;
                searchedBoardgame.Link = boardgame.Link ?? searchedBoardgame.Link;

                _context.Update(searchedBoardgame);

                return searchedBoardgame;
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
