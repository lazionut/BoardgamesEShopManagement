using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Addresses.Commands.UpdateAddress;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.API.Services;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ISingletonService _singletonService;

        public AddressesController(IMediator mediator, IMapper mapper, ISingletonService singletonService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _singletonService = singletonService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressPostPutDto updatedAddress)
        {
            GetAccountQuery queryAccount = new GetAccountQuery { AccountId = _singletonService.Id };

            Account? resultAccount = await _mediator.Send(queryAccount);

            UpdateAddressRequest commandAddress = new UpdateAddressRequest
            {
                AddressId = resultAccount.AddressId,
                AddressDetails = updatedAddress.Details,
                AddressCity = updatedAddress.City,
                AddressCounty = updatedAddress.County,
                AddressCountry = updatedAddress.Country,
                AddressPhone = updatedAddress.Phone
            };

            Address? resultAddress = await _mediator.Send(commandAddress);

            if (resultAddress == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
