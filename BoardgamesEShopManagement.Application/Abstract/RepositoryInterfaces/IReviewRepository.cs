using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>?> GetReviewsListPerBoardgame(int boardgameId, int pageIndex, int pageSize);
        Task<Review?> GetByAccountId(int accountId);
        Task<Review?> GetByBoardgameId(int boardgameId);
    }
}
