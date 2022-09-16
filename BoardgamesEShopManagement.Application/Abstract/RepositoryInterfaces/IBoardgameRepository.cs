using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IBoardgameRepository : IGenericRepository<Boardgame>
    {
        Task<List<Boardgame>?> GetBoardgamesSorted(int pageIndex, int pageSize, BoardgamesSortOrdersEnum sortOrder);
        Task<List<Boardgame>?> GetBoardgamesPerCategory(int categoryId, int pageIndex, int pageSize, BoardgamesSortOrdersEnum sortOrder);
        Task<List<Boardgame>?> GetBoardgamesByName(string characters, int pageIndex, int pageSize, BoardgamesSortOrdersEnum sortOrder);
    }
}
