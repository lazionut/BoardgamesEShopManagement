using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview;
using BoardgamesEShopManagement.Application.Categories.Queries.GetReview;
using BoardgamesEShopManagement.Application.Reviews.Commands.UpdateReview;
using BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReviewsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
                ReviewTitle = review.ReviewTitle,
                ReviewAuthor = review.ReviewAuthor,
                ReviewScore = review.ReviewScore,
                ReviewContent = review.ReviewContent,
                ReviewBoardgameId = review.ReviewBoardgameId,
                ReviewAccountId = review.ReviewAccountId
            };

            Review? result = await _mediator.Send(command);

            ReviewGetDto? mappedResult = _mapper.Map<ReviewGetDto>(result);

            if (mappedResult == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetReview), new { id = mappedResult.ReviewId }, mappedResult);
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

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewPatchDto updatedReview)
        {
            UpdateReviewRequest command = new UpdateReviewRequest
            {
                ReviewId = id,
                ReviewTitle = updatedReview.ReviewTitle,
                ReviewContent = updatedReview.ReviewContent,
            };

            Review? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
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
