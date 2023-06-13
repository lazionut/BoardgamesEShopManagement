using AutoMapper;
using BoardgamesEShopManagement.API.Controllers;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.UpdateOrderStatus;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrder;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersList;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListCounter;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

            if (order.BoardgameIds.Count != order.BoardgameQuantities.Count || order.BoardgameIds.Count == 0 || order.BoardgameQuantities.Count == 0)
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrders([BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetOrdersListQuery queryOrders = new GetOrdersListQuery
            {
                OrderPageIndex = pageIndex,
                OrderPageSize = pageSize
            };

            List<Order>? resultOrders = await _mediator.Send(queryOrders);

            if (resultOrders == null)
            {
                return NotFound();
            }

            List<OrderGetDto> mappedResultOrders = _mapper.Map<List<OrderGetDto>>(resultOrders);

            GetOrdersListCounterQuery commandOrdersCounter = new GetOrdersListCounterQuery { };

            int resultOrdersCounter = await _mediator.Send(commandOrdersCounter);

            if (resultOrdersCounter == 0)
            {
                return NotFound();
            }

            int mappedResultOrdersCounter = mappedResultOrders.Count();

            if (mappedResultOrdersCounter == 0)
            {
                return NotFound();
            }

            int pageCounter = resultOrdersCounter / mappedResultOrdersCounter;

            if (resultOrdersCounter % pageSize > 0)
            {
                ++pageCounter;
            }

            if (mappedResultOrdersCounter < pageSize)
            {
                pageCounter = pageIndex;
            }

            return Ok(new
            {
                pageCount = pageCounter,
                orders = mappedResultOrders
            });
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