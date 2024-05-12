using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IBoardgameRepository : IGenericRepository<Boardgame>
    {
        Task<List<Boardgame>?> GetAllSorted(int pageIndex, int pageSize, BoardgamesSortOrdersMode sortOrder);

        Task<int> GetAllSortedCounter();

        Task<List<Boardgame>?> GetPerCategory(int categoryId, int pageIndex, int pageSize, BoardgamesSortOrdersMode sortOrder);

        Task<int> GetPerCategoryCounter(int categoryId);

        Task<List<Boardgame>?> GetPerName(string characters, int pageIndex, int pageSize, BoardgamesSortOrdersMode sortOrder);

        Task<int> GetPerNameCounter(string characters);

        Task<List<string>> GetNames();
    }
}