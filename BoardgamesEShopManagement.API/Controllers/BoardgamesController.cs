﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByName;
using BoardgamesEShopManagement.Application.Addresses.Commands.CreateAddress;
using BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/boardgame")]
    [ApiController]
    public class BoardgamesController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public BoardgamesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoardgame([FromBody] BoardgamePostPutDto boardgame)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CreateBoardgameRequest command = new CreateBoardgameRequest
            {
                BoardgameImage = boardgame.BoardgameImage,
                BoardgameName = boardgame.BoardgameName,
                BoardgameDescription = boardgame.BoardgameDescription,
                BoardgamePrice = boardgame.BoardgamePrice,
                BoardgameLink = boardgame.BoardgameLink,
                BoardgameCategoryId = boardgame.BoardgameCategoryId
            };

            Boardgame result = await _mediator.Send(command);

            BoardgameGetDto mappedResult = _mapper.Map<BoardgameGetDto>(result);

            return CreatedAtAction(nameof(GetBoardgame), new { id = mappedResult.BoardgameId }, mappedResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetBoardgames()
        {
            List<Boardgame> result = await _mediator.Send(new GetBoardgamesListQuery());

            List<BoardgameGetDto> mappedResult = _mapper.Map<List<BoardgameGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet("/api/boardgame/search")]
        public async Task<IActionResult> GetBoardgamesByName([FromQuery] string keywords)
        {
            List<Boardgame> result = await _mediator.Send(new GetBoardgamesListByNameQuery { BoardgameNameCharacters = keywords });

            List<BoardgameGetDto> mappedResult = _mapper.Map<List<BoardgameGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBoardgame(int id)
        {
            GetBoardgameQuery query = new GetBoardgameQuery { BoardgameId = id };

            Boardgame result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            BoardgameGetDto mappedResult = _mapper.Map<BoardgameGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBoardgame(int id)
        {
            DeleteBoardgameRequest command = new DeleteBoardgameRequest { BoardgameId = id };

            bool result = await _mediator.Send(command);

            if (result == false)
                return NotFound();

            return Ok();
        }
    }
}
