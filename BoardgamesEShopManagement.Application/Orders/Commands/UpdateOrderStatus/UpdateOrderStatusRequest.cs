using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.Application.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusRequest : IRequest<Order>
    {
        public int OrderId { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
