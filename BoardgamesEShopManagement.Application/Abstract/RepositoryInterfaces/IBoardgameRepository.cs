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
        Task<List<Boardgame>> GetBoardgamesSorted(int pageIndex, int pageSize, string sortOrder);
        Task<List<Boardgame>> GetBoardgamesPerCategory(int categoryId, int pageIndex, int pageSize, string sortOrder);
        Task<List<Boardgame>> GetBoardgamesByName(string characters, int pageIndex, int pageSize, string sortOrder);
    }
}
