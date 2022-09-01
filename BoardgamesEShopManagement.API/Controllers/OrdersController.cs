﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Categories.Queries.GetOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderPostDto order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CreateOrderRequest command = new CreateOrderRequest
            {
                OrderAccountId = order.OrderAccountId,
                OrderBoardgameIds = order.OrderBoardgameIds,
                OrderBoardgameQuantities = order.OrderBoardgameQuantities
            };

            Order result = await _mediator.Send(command);

            OrderGetDto mappedResult = _mapper.Map<OrderGetDto>(result);

            return CreatedAtAction(nameof(GetOrder), new { id = mappedResult.OrderId }, mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            GetOrderQuery query = new GetOrderQuery { OrderId = id };

            Order result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            OrderGetDto mappedResult = _mapper.Map<OrderGetDto>(result);

            return Ok(mappedResult);
        }
    }
}