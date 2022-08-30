using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetWishlist
{
    public class GetWishlistQuery : IRequest<Wishlist>
    {
        public int WishlistId { get; set; }
    }
}