using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IBoardgameRepository : IGenericRepository<Boardgame>
    {
        Task<List<Boardgame>> GetBoardgamesPerCategory(int categoryId);
        Task<List<Boardgame>> GetBoardgamesByName(string characters);
        void WriteBoardgamesNames(string filePath, List<Boardgame> boardgamesList);
    }
}
