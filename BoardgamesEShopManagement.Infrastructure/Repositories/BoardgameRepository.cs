using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class BoardgameRepository : GenericRepository<Boardgame>, IBoardgameRepository
    {
        public List<Boardgame> GetBoardgamesPerCategory(int categoryId)
        {
            if (categoryId >= 0)
            {
                return genericItems.Where(boardgame => boardgame.CategoryId == categoryId).ToList();
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public Boardgame UpdateBoardgame(int boardgameId, Boardgame boardgame)
        {
            if (boardgameId >= 0)
            {
                Boardgame searchedBoardgame = genericItems.FirstOrDefault(boardgame => boardgame.Id == boardgameId);
                searchedBoardgame.CategoryId = boardgame.CategoryId;
                searchedBoardgame.Image = boardgame.Image ?? searchedBoardgame.Image;
                searchedBoardgame.Name = boardgame.Name ?? searchedBoardgame.Name;
                searchedBoardgame.Description = boardgame.Description ?? searchedBoardgame.Description;
                searchedBoardgame.Price = boardgame.Price;
                searchedBoardgame.Link = boardgame.Link ?? searchedBoardgame.Link;

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
