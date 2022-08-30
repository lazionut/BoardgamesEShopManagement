using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Categories.Queries.GetOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.DeleteOrder;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper)
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            DeleteOrderRequest command = new DeleteOrderRequest { OrderId = id };

            Order result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
