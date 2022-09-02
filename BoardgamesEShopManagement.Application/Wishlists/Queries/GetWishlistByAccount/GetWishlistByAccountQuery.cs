using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistByAccount
{
    public class GetWishlistByAccountQuery : IRequest<Wishlist>
    {
        public int WishlistAccountId { get; set; }
        public int WishlistId { get; set; }
    }
}