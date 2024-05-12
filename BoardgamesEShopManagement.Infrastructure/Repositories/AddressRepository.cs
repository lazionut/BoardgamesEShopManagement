using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class AddressRepository(ShopContext context, ILogger<AddressRepository> logger) : GenericRepository<Address>(context, logger), IAddressRepository
    {
    }
}