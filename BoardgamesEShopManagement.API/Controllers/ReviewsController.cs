using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReview;
using BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.API.Services;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ISingletonService _singletonService;

        public ReviewsController(IMediator mediator, IMapper mapper, ISingletonService singletonService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _singletonService = singletonService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewPostDto review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateReviewRequest command = new CreateReviewRequest
            {
                ReviewTitle = review.Title,
                ReviewAuthor = review.Author,
                ReviewScore = review.Score,
                ReviewContent = review.Content,
                ReviewBoardgameId = review.BoardgameId,
                ReviewAccountId = _singletonService.Id
            };

            Review? result = await _mediator.Send(command);

            ReviewGetDto? mappedResult = _mapper.Map<ReviewGetDto>(result);

            if (mappedResult == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetReview), new { id = mappedResult.Id }, mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            GetReviewQuery query = new GetReviewQuery { ReviewId = id };

            Review? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            ReviewGetDto mappedResult = _mapper.Map<ReviewGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            DeleteReviewRequest command = new DeleteReviewRequest { ReviewId = id };

            Review result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
