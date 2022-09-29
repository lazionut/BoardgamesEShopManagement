using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;
using BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategory;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList;
using BoardgamesEShopManagement.Application.Categories.Commands.UpdateCategory;
using BoardgamesEShopManagement.Application.Categories.Commands.DeleteCategory;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory;
using BoardgamesEShopManagement.API.Dto;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryPostPutDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateCategoryRequest command = new CreateCategoryRequest { CategoryName = category.Name };

            Category result = await _mediator.Send(command);

            CategoryGetDto mappedResult = _mapper.Map<CategoryGetDto>(result);

            return CreatedAtAction(nameof(GetCategory), new { id = mappedResult.Id }, mappedResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            GetCategoriesListQuery query = new GetCategoriesListQuery();

            List<Category>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<CategoryGetDto> mappedResult = _mapper.Map<List<CategoryGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            GetCategoryQuery query = new GetCategoryQuery { CategoryId = id };

            Category? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            CategoryGetDto mappedResult = _mapper.Map<CategoryGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/boardgames")]
        public async Task<IActionResult> GetBoardgamesPerCategory(int id, [BindRequired] int pageIndex, [BindRequired] int pageSize, [BindRequired] BoardgamesSortOrdersEnum sortOrder)
        {
            GetBoardgamesListPerCategoryQuery command = new GetBoardgamesListPerCategoryQuery
            {
                CategoryId = id,
                BoardgamePageIndex = pageIndex,
                BoardgamePageSize = pageSize,
                BoardgameSortOrder = sortOrder
            };

            List<Boardgame>? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            List<BoardgameGetDto> mappedResult = _mapper.Map<List<BoardgameGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryPostPutDto updatedCategory)
        {
            UpdateCategoryRequest command = new UpdateCategoryRequest
            {
                CategoryId = id,
                CategoryName = updatedCategory.Name
            };

            Category? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            DeleteCategoryRequest command = new DeleteCategoryRequest { CategoryId = id };

            Category? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
