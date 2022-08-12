using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class CreateWishlistRequest : IRequest<int>
    {
        public string WishlistName { get; set; } = null!;
        public List<WishlistBoardgameDto> WishlistBoardgames { get; set; } = null!;
    }
}
