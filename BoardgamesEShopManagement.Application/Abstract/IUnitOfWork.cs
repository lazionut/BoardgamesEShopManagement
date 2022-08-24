using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Application.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }
        IAccountRepository AccountRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBoardgameRepository BoardgameRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IWishlistRepository WishlistRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task Save();
    }
}
