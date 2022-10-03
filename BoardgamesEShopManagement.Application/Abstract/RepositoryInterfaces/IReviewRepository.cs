﻿using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>?> GetPerBoardgame(int boardgameId, int pageIndex, int pageSize);
        Task<int> GetPerBoardgameCounter(int boardgameId);
        Task<Review?> GetByAccountId(int accountId);
        Task<Review?> GetByBoardgameId(int boardgameId);
    }
}
