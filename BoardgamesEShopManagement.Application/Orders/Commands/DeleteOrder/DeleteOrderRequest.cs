using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderRequest : IRequest<Order>
    {
        public int OrderId { get; set; }
    }
}
