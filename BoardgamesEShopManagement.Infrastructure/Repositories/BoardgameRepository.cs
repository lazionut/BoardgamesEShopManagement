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
                searchedBoardgame.BoardgameImage = boardgame.BoardgameImage ?? searchedBoardgame.BoardgameImage;
                searchedBoardgame.BoardgameName = boardgame.BoardgameName ?? searchedBoardgame.BoardgameName;
                searchedBoardgame.BoardgameDescription = boardgame.BoardgameDescription ?? searchedBoardgame.BoardgameDescription;
                searchedBoardgame.BoardgamePrice = boardgame.BoardgamePrice;
                searchedBoardgame.BoardgameLink = boardgame.BoardgameLink ?? searchedBoardgame.BoardgameLink;

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
                    boardgameStreamWriter.WriteLine(boardgame.BoardgameName);
                }
            }
        }
    }
}
