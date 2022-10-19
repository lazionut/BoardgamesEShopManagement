using MediatR;
using Microsoft.Extensions.DependencyInjection;

using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Application.Abstract;
using BoardgamesEShopManagement.Infrastructure.Repositories;
using BoardgamesEShopManagement.Infrastructure;

namespace BoardgamesEShopManagement.ConsolePresentation
{
    internal static class MediatorInitializer
    {
        internal static IMediator Init()
        {
            var diContainer = new ServiceCollection()
                .AddMediatR(typeof(IBoardgameRepository))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IAddressRepository, AddressRepository>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IBoardgameRepository, BoardgameRepository>()
                .AddScoped<IReviewRepository, ReviewRepository>()
                .AddScoped<IWishlistRepository, WishlistRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .BuildServiceProvider();

            return diContainer.GetRequiredService<IMediator>();
        }
    }
}
