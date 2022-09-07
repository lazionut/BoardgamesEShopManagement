﻿using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderRequest : IRequest<Order>
    {
        public int OrderAccountId { get; set; }
        public List<int> OrderBoardgameIds { get; set; } = null!;
        public List<int> OrderBoardgameQuantities { get; set; } = null!;
    }
}
