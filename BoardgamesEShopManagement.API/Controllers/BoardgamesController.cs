using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListSorted;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByName;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame;
using BoardgamesEShopManagement.API.Dto;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/boardgames")]
    [ApiController]
    public class BoardgamesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BoardgamesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBoardgame([FromBody] BoardgamePostPutDto boardgame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateBoardgameRequest command = new CreateBoardgameRequest
            {
                BoardgameImage = boardgame.Image,
                BoardgameName = boardgame.Name,
                BoardgameReleaseYear = boardgame.ReleaseYear,
                BoardgameDescription = boardgame.Description,
                BoardgamePrice = boardgame.Price,
                BoardgameLink = boardgame.Link,
                BoardgameQuantity = boardgame.Quantity,
                BoardgameCategoryId = boardgame.CategoryId,
            };

            Boardgame result = await _mediator.Send(command);

            BoardgameGetDto? mappedResult = _mapper.Map<BoardgameGetDto>(result);

            if (mappedResult == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetBoardgame), new { id = mappedResult.Id }, mappedResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetBoardgames([BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetBoardgamesListQuery query = new GetBoardgamesListQuery
            {
                BoardgamePageIndex = pageIndex,
                BoardgamePageSize = pageSize
            };

            List<Boardgame>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<BoardgameGetDto> mappedResult = _mapper.Map<List<BoardgameGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet("sorted")]
        public async Task<IActionResult> GetBoardgamesSorted
            ([BindRequired] int pageIndex, [BindRequired] int pageSize, [BindRequired] BoardgamesSortOrdersEnum sortOrder)
        {
            GetBoardgamesListSortedQuery query = new GetBoardgamesListSortedQuery
            {
                BoardgamePageIndex = pageIndex,
                BoardgamePageSize = pageSize,
                BoardgameSortOrder = sortOrder
            };

            List<Boardgame>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<BoardgameGetDto> mappedResult = _mapper.Map<List<BoardgameGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBoardgamesByName
            ([BindRequired] string keywords, [BindRequired] int pageIndex, [BindRequired] int pageSize, [BindRequired] BoardgamesSortOrdersEnum sortOrder)
        {
            GetBoardgamesListByNameQuery query = new GetBoardgamesListByNameQuery
            {
                BoardgameNameCharacters = keywords,
                BoardgamePageIndex = pageIndex,
                BoardgamePageSize = pageSize,
                BoardgameSortOrder = sortOrder
            };

            List<Boardgame>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<BoardgameGetDto> mappedResult = _mapper.Map<List<BoardgameGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBoardgame(int id)
        {
            GetBoardgameQuery query = new GetBoardgameQuery { BoardgameId = id };

            Boardgame? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            BoardgameGetDto mappedResult = _mapper.Map<BoardgameGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> GetReviewsPerBoardgame(int id, [BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetReviewsListPerBoardgameQuery query = new GetReviewsListPerBoardgameQuery
            {
                ReviewBoardgameId = id,
                ReviewPageIndex = pageIndex,
                ReviewPageSize = pageSize
            };

            List<Review>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<ReviewGetDto> mappedResult = _mapper.Map<List<ReviewGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBoardgame(int id, [FromBody] BoardgamePostPutDto updatedBoardgame)
        {
            UpdateBoardgameRequest command = new UpdateBoardgameRequest
            {
                BoardgameId = id,
                BoardgameImage = updatedBoardgame.Image,
                BoardgameName = updatedBoardgame.Name,
                BoardgameReleaseYear = updatedBoardgame.ReleaseYear,
                BoardgameDescription = updatedBoardgame.Description,
                BoardgamePrice = updatedBoardgame.Price,
                BoardgameLink = updatedBoardgame.Link,
                BoardgameQuantity = updatedBoardgame.Quantity,
                BoardgameCategoryId = updatedBoardgame.CategoryId
            };

            Boardgame? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBoardgame(int id)
        {
            DeleteBoardgameRequest command = new DeleteBoardgameRequest { BoardgameId = id };

            Boardgame? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}/archive")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ArchiveBoardgame(int id)
        {
            ArchiveBoardgameRequest command = new ArchiveBoardgameRequest { BoardgameId = id };

            Boardgame? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
