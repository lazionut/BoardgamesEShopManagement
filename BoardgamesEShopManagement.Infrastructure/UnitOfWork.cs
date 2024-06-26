﻿using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _shopContext;
        private bool disposed = false;

        public UnitOfWork(ShopContext shopContext, IAddressRepository addressRepository,
            IAccountRepository accountRepository, ICategoryRepository categoryRepository,
            IBoardgameRepository boardgameRepository, IReviewRepository reviewRepository,
            IWishlistRepository wishlistRepository, IOrderRepository orderRepository)
        {
            _shopContext = shopContext;
            AddressRepository = addressRepository;
            AccountRepository = accountRepository;
            CategoryRepository = categoryRepository;
            BoardgameRepository = boardgameRepository;
            ReviewRepository = reviewRepository;
            WishlistRepository = wishlistRepository;
            OrderRepository = orderRepository;
        }

        public IAddressRepository AddressRepository { get; private set; }
        public IAccountRepository AccountRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IBoardgameRepository BoardgameRepository { get; private set; }
        public IReviewRepository ReviewRepository { get; private set; }
        public IWishlistRepository WishlistRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }

        public async Task Save()
        {
            await _shopContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && _shopContext != null)
                {
                    _shopContext.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}