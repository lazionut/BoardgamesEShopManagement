﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Addresses.Queries.GetAddress
{
    public class GetAddressQuery : IRequest<Address>
    {
        public int AddressId { get; set; }
    }
}