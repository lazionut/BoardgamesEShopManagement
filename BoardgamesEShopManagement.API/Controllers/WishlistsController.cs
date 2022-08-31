using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist;
using BoardgamesEShopManagement.Application.Categories.Queries.GetWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist;
using BoardgamesEShopManagement.Application.Orders.Commands.DeleteWishlistItem;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlistItem;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/wishlists")]
    [ApiController]
    public class WishlistsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WishlistsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWishlist([FromBody] WishlistPostDto wishlist)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CreateWishlistRequest command = new CreateWishlistRequest
            {
                WishlistName = wishlist.WishlistName,
                WishlistAccountId = wishlist.WishlistAccountId,
                WishlistBoardgameIds = wishlist.WishlistBoardgameIds
            };

            Wishlist result = await _mediator.Send(command);

            WishlistGetDto mappedResult = _mapper.Map<WishlistGetDto>(result);

            return CreatedAtAction(nameof(GetWishlist), new { id = mappedResult.WishlistId }, mappedResult);
        }

        [HttpPost]
        [Route("boardgame")]
        public async Task<IActionResult> CreateWishlistItem([FromBody] WishlistItemPostDto wishlistItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CreateWishlistItemRequest command = new CreateWishlistItemRequest
            {
                WishlistId = wishlistItem.WishlistId,
                WishlistBoardgameId = wishlistItem.WishlistBoardgameId
            };

            Wishlist result = await _mediator.Send(command);

            WishlistGetDto mappedResult = _mapper.Map<WishlistGetDto>(result);

            return CreatedAtAction(nameof(GetWishlist), new { id = mappedResult.WishlistId }, mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetWishlist(int id)
        {
            GetWishlistQuery query = new GetWishlistQuery { WishlistId = id };

            Wishlist result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            WishlistGetDto mappedResult = _mapper.Map<WishlistGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWishlist(int id)
        {
            DeleteWishlistRequest command = new DeleteWishlistRequest { WishlistId = id };

            Wishlist result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Route("{wishlistId}/boardgames/{boardgameId}")]
        public async Task<IActionResult> DeleteWishlistItem(int wishlistId, int boardgameId)
        {
            DeleteWishlistItemRequest command = new DeleteWishlistItemRequest 
            { 
                WishlistId = wishlistId,
                BoardgameId = boardgameId
            };

            bool result = await _mediator.Send(command);

            if (result == false)
                return NotFound();

            return Ok();
        }
    }
}
