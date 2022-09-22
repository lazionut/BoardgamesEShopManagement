using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.UpdateOrderStatus;

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
            {
                return BadRequest(ModelState);
            }

            CreateOrderRequest command = new CreateOrderRequest
            {
                OrderAccountId = order.AccountId,
                OrderBoardgameIds = order.BoardgameIds,
                OrderBoardgameQuantities = order.BoardgameQuantities,
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
