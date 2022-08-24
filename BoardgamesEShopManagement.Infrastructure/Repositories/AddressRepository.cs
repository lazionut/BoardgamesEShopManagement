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

        public async Task<Address> UpdateAddress(int addressId, Address address)
        {
            if (addressId >= 0)
            {
                Address searchedAddress = await _context.Addresses.SingleOrDefaultAsync(address => address.Id == addressId);
                searchedAddress.Details = address.Details ?? searchedAddress.Details;
                searchedAddress.City = address.City ?? searchedAddress.City;
                searchedAddress.County = address.County ?? searchedAddress.County;
                searchedAddress.Country = address.Country ?? searchedAddress.Country;
                searchedAddress.Phone = address.Phone ?? searchedAddress.Phone;

                _context.Update(searchedAddress);

                return searchedAddress;
            }
            else
            {
                throw new NegativeIdException();
            }
        }
    }
}
