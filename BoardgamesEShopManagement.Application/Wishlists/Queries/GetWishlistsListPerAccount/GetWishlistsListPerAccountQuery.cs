﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetWishlistsListPerAccount
{
    public class GetWishlistsListPerAccountQuery : IRequest<List<Wishlist>>
    {
        public int AccountId { get; set; }
    }
}