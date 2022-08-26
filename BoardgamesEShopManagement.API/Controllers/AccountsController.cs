using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount;
using BoardgamesEShopManagement.Application.Addresses.Commands.DeleteAddress;
using BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public AccountsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountPostDto account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CreateAccountRequest command = new CreateAccountRequest
            {
                AccountAddressId = account.AccountAddressId,
                AccountFirstName = account.AccountFirstName,
                AccountLastName = account.AccountLastName,
                AccountEmail = account.AccountEmail,
                AccountPassword = account.AccountPassword,
            };

            Account result = await _mediator.Send(command);

            AccountGetDto mappedResult = _mapper.Map<AccountGetDto>(result);

            return CreatedAtAction(nameof(GetAccount), new { id = mappedResult.AccountId }, mappedResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            List<Account> result = await _mediator.Send(new GetAccountsListQuery());

            List<AccountGetDto> mappedResult = _mapper.Map<List<AccountGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            GetAccountQuery query = new GetAccountQuery { AccountId = id };

            Account result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            AccountGetDto mappedResult = _mapper.Map<AccountGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountPutDto updatedAccount)
        {
            UpdateAccountRequest command = new UpdateAccountRequest
            {
                AccountId = id,
                AccountFirstName = updatedAccount.AccountFirstName,
                AccountLastName = updatedAccount.AccountLastName,
                AccountEmail = updatedAccount.AccountEmail,
                AccountPassword = updatedAccount.AccountPassword,
            };

            Account result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            DeleteAddressRequest command = new DeleteAddressRequest { AddressId = id };

            bool result = await _mediator.Send(command);

            if (result == false)
                return NotFound();

            return Ok();
        }
    }
}
