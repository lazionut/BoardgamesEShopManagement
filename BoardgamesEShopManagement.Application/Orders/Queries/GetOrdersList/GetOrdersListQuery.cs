using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistList
{
    public class GetOrdersListQuery : IRequest<IEnumerable<OrderListVm>>
    {

    }
}
