using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly ShopContext _context;
        private readonly ILogger<Address> _logger;

        public AddressRepository(ShopContext context, ILogger<Address> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}