using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using MediatR;

namespace BoardgamesEShopManagement.Application.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusRequest : IRequest<Order>
    {
        public int OrderId { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}