using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>> GetReviewsListPerBoardgame(int boardgameId);
    }
}
