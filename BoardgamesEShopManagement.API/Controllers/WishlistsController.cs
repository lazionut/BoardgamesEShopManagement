using AutoMapper;
using BoardgamesEShopManagement.API.Controllers;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlist;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/wishlists")]
    [ApiController]
    [Authorize]
    public class WishlistsController : CustomControllerBase
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

            if (wishlist.BoardgameIds.Count == 0)
            {
                return NotFound();
            }

            CreateWishlistRequest command = new CreateWishlistRequest
            {
                WishlistAccountId = GetAccountId(),
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
    }
}