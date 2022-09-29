using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistByAccount;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistsListPerAccount;
using BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListPerAccount;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrderByAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.ArchiveAccount;
using BoardgamesEShopManagement.Application.Accounts.Queries.LoginAccount;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetByEmail;
using BoardgamesEShopManagement.Application.Accounts.Commands.AddRoleToAccount;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.API.Services;
using BoardgamesEShopManagement.Application.Addresses.Commands.CreateAddress;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ISingletonService _singletonService;

        public AccountsController(IMediator mediator, IMapper mapper, ISingletonService singletonService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _singletonService = singletonService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountPostDto account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateAddressRequest commandAddress = new CreateAddressRequest
            {
                AddressDetails = account.Details,
                AddressCity = account.City,
                AddressCounty = account.County,
                AddressCountry = account.Country,
                AddressPhone = account.Phone
            };

            Address resultAddress = await _mediator.Send(commandAddress);

            CreateAccountRequest commandAccount = new CreateAccountRequest
            {
                AccountFirstName = account.FirstName,
                AccountLastName = account.LastName,
                AccountEmail = account.Email,
                AccountPassword = account.Password,
                AccountAddressId = resultAddress.Id
            };

            Account resultAccount = await _mediator.Send(commandAccount);

            AccountGetDto? mappedResultAccount = _mapper.Map<AccountGetDto>(resultAccount);

            if (mappedResultAccount == null)
            {
                return NotFound();
            }

            if (account.IsAdmin == false)
            {
                AddRoleToAccountRequest roleCommand = new AddRoleToAccountRequest
                {
                    Email = account.Email,
                    RoleName = "Customer"
                };

                await _mediator.Send(roleCommand);
            }
            else
            {
                AddRoleToAccountRequest roleCommand = new AddRoleToAccountRequest
                {
                    Email = account.Email,
                    RoleName = "Admin"
                };

                await _mediator.Send(roleCommand);
            }

            LoginAccountQuery query = new LoginAccountQuery
            {
                AccountEmail = account.Email,
                AccountPassword = account.Password
            };

            string tokenResult = await _mediator.Send(query);

            _singletonService.Id = resultAccount.Id;

            return Ok(new
            {
                token = tokenResult
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] AccountLoginDto account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginAccountQuery query = new LoginAccountQuery
            {
                AccountEmail = account.Email,
                AccountPassword = account.Password
            };

            string result = await _mediator.Send(query);

            if (result == "false")
            {
                return NotFound();
            }

            GetAccountByEmailQuery emailQuery = new GetAccountByEmailQuery
            {
                AccountEmail = account.Email,
            };

            Account emailResult = await _mediator.Send(emailQuery);

            _singletonService.Id = emailResult.Id;

            return Ok(new
            {
                token = result
            });
        }

        /* 
         * if more roles are needed
         * 
        [HttpPost]
        [Route("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToAccount([FromBody] AccountRoleDto account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GetAccountByEmailQuery query = new GetAccountByEmailQuery
            {
                AccountEmail = account.Email,
            };

            Account? searchedAccount = await _mediator.Send(query);

            if (searchedAccount == null)
            {
                return NotFound();
            }

            AddRoleToAccountRequest command = new AddRoleToAccountRequest
            {
                Email = account.Email,
                RoleName = account.RoleName
            };

            bool result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("Failed to add role to user");
            }

            return Ok($"User added successfully to {account.RoleName} role");
        }
        */

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAccounts([BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetAccountsListQuery query = new GetAccountsListQuery
            {
                AccountPageIndex = pageIndex,
                AccountPageSize = pageSize
            };

            List<Account>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<AccountAdminGetDto> mappedResult = _mapper.Map<List<AccountAdminGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetAccount()
        {
            GetAccountQuery query = new GetAccountQuery { AccountId = _singletonService.Id };

            Account? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            AccountGetDto mappedResult = _mapper.Map<AccountGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("orders/{orderId}")]
        public async Task<IActionResult> GetOrderByAccount(int orderId)
        {
            GetOrderByAccountQuery query = new GetOrderByAccountQuery { AccountId = _singletonService.Id, OrderId = orderId };

            Order? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            OrderGetDto mappedResult = _mapper.Map<OrderGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> GetOrdersPerAccount([BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetOrdersListPerAccountQuery query = new GetOrdersListPerAccountQuery
            {
                OrderAccountId = _singletonService.Id,
                OrderPageIndex = pageIndex,
                OrderPageSize = pageSize
            };

            List<Order>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<OrderGetDto> mappedResult = _mapper.Map<List<OrderGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("wishlists/{wishlistId}")]
        public async Task<IActionResult> GetWishlistByAccount(int wishlistId)
        {
            GetWishlistByAccountQuery query = new GetWishlistByAccountQuery
            {
                WishlistAccountId = _singletonService.Id,
                WishlistId = wishlistId
            };

            Wishlist? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            WishlistGetDto mappedResult = _mapper.Map<WishlistGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("wishlists")]
        public async Task<IActionResult> GetWishlistsPerAccount([BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetWishlistsListPerAccountQuery query = new GetWishlistsListPerAccountQuery
            {
                WishlistAccountId = _singletonService.Id,
                WishlistPageIndex = pageIndex,
                WishlistPageSize = pageSize
            };

            List<Wishlist>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<WishlistGetDto> mappedResult = _mapper.Map<List<WishlistGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountPutDto updatedAccount)
        {
            UpdateAccountRequest command = new UpdateAccountRequest
            {
                AccountId = _singletonService.Id,
                AccountEmail = updatedAccount.Email,
                AccountFirstName = updatedAccount.FirstName,
                AccountLastName = updatedAccount.LastName,
            };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /*
        [HttpPatch]
        [Route("change-password")]
        public async Task<IActionResult> UpdateAccountPassword([FromBody] AccountPasswordPatchDto updatedAccount)
        {
            UpdateAccountRequest command = new UpdateAccountRequest
            {
                AccountId = _singletonService.Id,
                AccountPassword = updatedAccount.Password,
            };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        */

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            DeleteAccountRequest command = new DeleteAccountRequest { AccountId = id };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("archive")]
        public async Task<IActionResult> ArchiveAccount()
        {
            ArchiveAccountRequest command = new ArchiveAccountRequest { AccountId = _singletonService.Id };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("wishlists/{wishlistId}")]
        public async Task<IActionResult> DeleteWishlistByAccount(int wishlistId)
        {
            DeleteWishlistRequest command = new DeleteWishlistRequest
            {
                WishlistAccountId = _singletonService.Id,
                WishlistId = wishlistId
            };

            Wishlist? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
