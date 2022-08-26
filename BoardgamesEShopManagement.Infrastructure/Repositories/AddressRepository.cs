using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly ShopContext _context;

        public AddressRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
    }
}
