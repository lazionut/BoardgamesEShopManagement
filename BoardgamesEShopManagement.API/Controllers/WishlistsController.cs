using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Commands.UpdateWishlist;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/wishlists")]
    [ApiController]
    [Authorize(Roles = "Customer")]
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
            {
                return BadRequest(ModelState);
            }

            CreateWishlistRequest command = new CreateWishlistRequest
            {
                WishlistAccountId = wishlist.AccountId,
                WishlistName = wishlist.Name,
                WishlistBoardgameIds = wishlist.BoardgameIds
            };

            Wishlist result = await _mediator.Send(command);

            WishlistGetDto? mappedResult = _mapper.Map<WishlistGetDto>(result);

            if (mappedResult == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetWishlist), new { id = mappedResult.Id }, mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetWishlist(int id)
        {
            GetWishlistQuery query = new GetWishlistQuery { WishlistId = id };

            Wishlist? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            WishlistGetDto mappedResult = _mapper.Map<WishlistGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateWishlist(int id, [FromBody] WishlistPutDto wishlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UpdateWishlistRequest command = new UpdateWishlistRequest
            {
                WishlistId = id,
                WishlistName = wishlist.Name,
                WishlistAccountId = wishlist.AccountId,
                WishlistBoardgameIds = wishlist.BoardgameIds
            };

            Wishlist result = await _mediator.Send(command);

            WishlistGetDto? mappedResult = _mapper.Map<WishlistGetDto>(result);

            if (mappedResult == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
