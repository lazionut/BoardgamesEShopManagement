using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist
{
    public class DeleteWishlistRequest : IRequest<bool>
    {
        public int WishlistId { get; set; }
    }
}
