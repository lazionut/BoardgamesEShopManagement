using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<int> GetCategoryCounter();
    }
}