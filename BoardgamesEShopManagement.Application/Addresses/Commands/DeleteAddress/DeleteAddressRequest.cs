using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressRequest : IRequest<Address>
    {
        public int AddressId { get; set; }
    }
}
