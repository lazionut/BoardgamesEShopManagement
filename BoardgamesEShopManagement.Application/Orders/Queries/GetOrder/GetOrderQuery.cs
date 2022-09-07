﻿using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<Order>
    {
        public int OrderId { get; set; }
    }
}