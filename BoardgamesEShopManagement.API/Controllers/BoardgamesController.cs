using AutoMapper;
using BoardgamesEShopManagement.API.Controllers;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByName;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByNameCounter;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListNames;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategoryCounter;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgame;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerBoardgameCounter;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/boardgames")]
    [ApiController]
    public class BoardgamesController : CustomControllerBase
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
        public async Task<IActionResult> GetBoardgames([BindRequired] int pageIndex, [BindRequired] int pageSize, [BindRequired] BoardgamesSortOrdersEnum sortOrder)
        {
            GetBoardgamesListQuery queryBoardgame = new GetBoardgamesListQuery
            {
                BoardgamePageIndex = pageIndex,
                BoardgamePageSize = pageSize,
                BoardgameSortOrder = sortOrder
            };

            List<Boardgame>? resultBoardgame = await _mediator.Send(queryBoardgame);

            if (resultBoardgame == null)
            {
                return NotFound();
            }

            List<BoardgameGetDto> mappedResultBoardgames = _mapper.Map<List<BoardgameGetDto>>(resultBoardgame);

            GetBoardgamesListCounterQuery commandBoardgameCounter = new GetBoardgamesListCounterQuery { };

            int resultBoardgameCounter = await _mediator.Send(commandBoardgameCounter);

            if (resultBoardgameCounter == 0)
            {
                return NotFound();
            }

            int mappedResultBoardgameCounter = mappedResultBoardgames.Count();

            if (mappedResultBoardgameCounter == 0)
            {
                return NotFound();
            }

            int pageCounter = resultBoardgameCounter / mappedResultBoardgameCounter;

            if (resultBoardgameCounter % pageSize > 0)
            {
                ++pageCounter;
            }

            if (mappedResultBoardgameCounter < pageSize)
            {
                pageCounter = pageIndex;
            }

            return Ok(new
            {
                pageCount = pageCounter,
                boardgames = mappedResultBoardgames
            });
        }

        [HttpGet]
        [Route("category/{id}")]
        public async Task<IActionResult> GetBoardgamesPerCategory(int id, [BindRequired] int pageIndex, [BindRequired] int pageSize, [BindRequired] BoardgamesSortOrdersEnum sortOrder)
        {
            GetBoardgamesListPerCategoryQuery commandBoardgame = new GetBoardgamesListPerCategoryQuery
            {
                CategoryId = id,
                BoardgamePageIndex = pageIndex,
                BoardgamePageSize = pageSize,
                BoardgameSortOrder = sortOrder
            };

            List<Boardgame>? resultBoardgame = await _mediator.Send(commandBoardgame);

            if (resultBoardgame == null)
            {
                return NotFound();
            }

            List<BoardgameGetDto> mappedResultBoardgames = _mapper.Map<List<BoardgameGetDto>>(resultBoardgame);

            GetBoardgamesListPerCategoryCounterQuery commandBoardgameCounter = new GetBoardgamesListPerCategoryCounterQuery
            {
                CategoryId = id,
            };

            int resultBoardgameCounter = await _mediator.Send(commandBoardgameCounter);

            if (resultBoardgameCounter == 0)
            {
                return NotFound();
            }

            int mappedResultBoardgameCounter = mappedResultBoardgames.Count();

            if (mappedResultBoardgameCounter == 0)
            {
                return NotFound();
            }

            int pageCounter = resultBoardgameCounter / mappedResultBoardgameCounter;

            if (resultBoardgameCounter % pageSize > 0)
            {
                ++pageCounter;
            }

            if (mappedResultBoardgameCounter < pageSize)
            {
                pageCounter = pageIndex;
            }

            return Ok(new
            {
                pageCount = pageCounter,
                boardgames = mappedResultBoardgames
            });
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBoardgamesByName
            ([BindRequired] string keywords, [BindRequired] int pageIndex, [BindRequired] int pageSize, [BindRequired] BoardgamesSortOrdersEnum sortOrder)
        {
            GetBoardgamesListByNameQuery queryBoardgames = new GetBoardgamesListByNameQuery
            {
                BoardgameNameCharacters = keywords,
                BoardgamePageIndex = pageIndex,
                BoardgamePageSize = pageSize,
                BoardgameSortOrder = sortOrder
            };

            List<Boardgame>? resultBoardgames = await _mediator.Send(queryBoardgames);

            if (resultBoardgames == null)
            {
                return NotFound();
            }

            List<BoardgameGetDto> mappedResultBoardgames = _mapper.Map<List<BoardgameGetDto>>(resultBoardgames);

            GetBoardgamesListByNameCounterQuery commandBoardgamesCounter = new GetBoardgamesListByNameCounterQuery
            {
                BoardgameNameCharacters = keywords,
            };

            int resultBoardgamesCounter = await _mediator.Send(commandBoardgamesCounter);

            if (resultBoardgamesCounter == 0)
            {
                return NotFound();
            }

            int mappedResultBoardgameCounter = mappedResultBoardgames.Count();

            if (mappedResultBoardgameCounter == 0)
            {
                return NotFound();
            }

            int pageCounter = resultBoardgamesCounter / mappedResultBoardgameCounter;

            if (resultBoardgamesCounter % pageSize > 0)
            {
                ++pageCounter;
            }

            if (mappedResultBoardgameCounter < pageSize)
            {
                pageCounter = pageIndex;
            }

            return Ok(new
            {
                pageCount = pageCounter,
                boardgames = mappedResultBoardgames
            });
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
            GetReviewsListPerBoardgameQuery queryReviews = new GetReviewsListPerBoardgameQuery
            {
                ReviewBoardgameId = id,
                ReviewPageIndex = pageIndex,
                ReviewPageSize = pageSize
            };

            List<Review>? resultReviews = await _mediator.Send(queryReviews);

            if (resultReviews == null)
            {
                return NotFound();
            }

            List<ReviewGetDto> mappedResultReviews = _mapper.Map<List<ReviewGetDto>>(resultReviews);

            GetReviewsListPerBoardgameCounterQuery commandReviewsCounter = new GetReviewsListPerBoardgameCounterQuery { };

            int resultReviewsCounter = await _mediator.Send(commandReviewsCounter);

            if (resultReviewsCounter == 0)
            {
                return NotFound();
            }

            int mappedResultReviewsCounter = mappedResultReviews.Count();

            if (mappedResultReviewsCounter == 0)
            {
                return NotFound();
            }

            int pageCounter = resultReviewsCounter / mappedResultReviewsCounter;

            if (resultReviewsCounter % pageSize > 0)
            {
                ++pageCounter;
            }

            if (mappedResultReviewsCounter < pageSize)
            {
                pageCounter = pageIndex;
            }

            return Ok(new
            {
                pageCount = pageCounter,
                reviews = mappedResultReviews
            });
        }

        [HttpGet]
        [Route("names")]
        public async Task<IActionResult> GetBoardgamesNames()
        {
            GetBoardgamesListNamesQuery query = new GetBoardgamesListNamesQuery { };

            List<string> result = await _mediator.Send(query);

            return Ok(new
            {
                names = result
            });
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