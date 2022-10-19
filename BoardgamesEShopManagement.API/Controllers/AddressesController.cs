using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Addresses.Commands.UpdateAddress;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.API.Controllers;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    [Authorize]
    public class AddressesController : CustomControllerBase
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressPostPutDto updatedAddress)
        {
            GetAccountQuery queryAccount = new GetAccountQuery { AccountId = GetAccountId() };

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
