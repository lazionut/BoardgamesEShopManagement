using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist
{
    public class CreateOrder : IRequest<int>
    {
        public string BuyerName { get; set; } = null!;
        public List<OrderDto> OrderItems { get; set; } = null!;
    }
}
