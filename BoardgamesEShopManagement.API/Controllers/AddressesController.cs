using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Addresses.Commands.CreateAddress;
using BoardgamesEShopManagement.Application.Addresses.Queries.GetAddress;
using BoardgamesEShopManagement.Application.Addresses.Commands.UpdateAddress;
using BoardgamesEShopManagement.Application.Addresses.Commands.DeleteAddress;
using BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveAddress;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddressesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressPostPutDto address)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CreateAddressRequest command = new CreateAddressRequest
            {
                AddressDetails = address.AddressDetails,
                AddressCity = address.AddressCity,
                AddressCounty = address.AddressCounty,
                AddressCountry = address.AddressCountry,
                AddressPhone = address.AddressPhone
            };

            Address result = await _mediator.Send(command);

            AddressGetDto mappedResult = _mapper.Map<AddressGetDto>(result);

            return CreatedAtAction(nameof(GetAddress), new { id = mappedResult.AddressId }, mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            GetAddressQuery query = new GetAddressQuery { AddressId = id };

            Address result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            AddressGetDto mappedResult = _mapper.Map<AddressGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressPostPutDto updatedAddress)
        {
            UpdateAddressRequest command = new UpdateAddressRequest
            {
                AddressId = id,
                AddressDetails = updatedAddress.AddressDetails,
                AddressCity = updatedAddress.AddressCity,
                AddressCounty = updatedAddress.AddressCounty,
                AddressCountry = updatedAddress.AddressCountry,
                AddressPhone = updatedAddress.AddressPhone
            };

            Address result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            DeleteAddressRequest command = new DeleteAddressRequest { AddressId = id };

            Address result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok();
        }


        [HttpDelete]
        [Route("{id}/archive")]
        public async Task<IActionResult> ArchiveAddress(int id)
        {
            ArchiveAddressRequest command = new ArchiveAddressRequest { AddressId = id };

            Address result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
