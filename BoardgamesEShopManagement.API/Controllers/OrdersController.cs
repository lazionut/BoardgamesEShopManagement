using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.UpdateOrderStatus;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.API.Controllers;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : CustomControllerBase
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
            {
                return BadRequest(ModelState);
            }

            if (order.BoardgameIds.Count != order.BoardgameQuantities.Count)
            {
                return NotFound();
            }

            CreateOrderRequest command = new CreateOrderRequest
            {
                OrderAccountId = GetAccountId(),
                OrderBoardgameIds = order.BoardgameIds,
                OrderBoardgameQuantities = order.BoardgameQuantities,
                OrderFullName = order.FullName,
                OrderAddress = order.Address
            };

            Order result = await _mediator.Send(command);

            OrderGetDto? mappedResult = _mapper.Map<OrderGetDto>(result);

            if (mappedResult == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetOrder), new { id = mappedResult.Id }, mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            GetOrderQuery query = new GetOrderQuery { OrderId = id };

            Order? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            OrderGetDto mappedResult = _mapper.Map<OrderGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpPatch]
        [Route("{id}/change-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromQuery] OrderStatusEnum orderStatus)
        {
            UpdateOrderStatusRequest command = new UpdateOrderStatusRequest
            {
                OrderId = id,
                OrderStatus = orderStatus,
            };

            Order? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
