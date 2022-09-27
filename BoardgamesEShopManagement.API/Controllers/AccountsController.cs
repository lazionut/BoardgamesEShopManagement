using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerAccount;
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

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountPostDto account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateAccountRequest command = new CreateAccountRequest
            {
                AccountFirstName = account.FirstName,
                AccountLastName = account.LastName,
                AccountEmail = account.Email,
                AccountPassword = account.Password,
                AccountAddressId = account.AddressId
            };

            Account result = await _mediator.Send(command);

            AccountGetDto? mappedResult = _mapper.Map<AccountGetDto>(result);

            if (mappedResult == null)
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
                Email = account.Email,
                Password = account.Password
            };

            string tokenResult = await _mediator.Send(query);

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
                Email = account.Email,
                Password = account.Password
            };

            string result = await _mediator.Send(query);

            if (result == "false")
            {
                return Unauthorized();
            }

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

            List<AccountGetDto> mappedResult = _mapper.Map<List<AccountGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAccount(int id)
        {
            GetAccountQuery query = new GetAccountQuery { AccountId = id };

            Account? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            AccountGetDto mappedResult = _mapper.Map<AccountGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/reviews")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewsPerAccount(int id, [BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetReviewsListPerAccountQuery query = new GetReviewsListPerAccountQuery
            {
                ReviewAccountId = id,
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

        [HttpGet]
        [Route("{accountId}/orders/{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrderByAccount(int accountId, int orderId)
        {
            GetOrderByAccountQuery query = new GetOrderByAccountQuery { AccountId = accountId, OrderId = orderId };

            Order? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            OrderGetDto mappedResult = _mapper.Map<OrderGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/orders")]
        [Authorize]
        public async Task<IActionResult> GetOrdersPerAccount(int id, [BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetOrdersListPerAccountQuery query = new GetOrdersListPerAccountQuery
            {
                OrderAccountId = id,
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
        [Route("{accountId}/wishlists/{wishlistId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetWishlistByAccount(int accountId, int wishlistId)
        {
            GetWishlistByAccountQuery query = new GetWishlistByAccountQuery
            {
                WishlistAccountId = accountId,
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
        [Route("{id}/wishlists")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetWishlistsPerAccount(int id, [BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetWishlistsListPerAccountQuery query = new GetWishlistsListPerAccountQuery
            {
                WishlistAccountId = id,
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

        [HttpPatch]
        [Route("{id}/change-name")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateAccountName(int id, [FromBody] AccountNamePatchDto updatedAccount)
        {
            UpdateAccountRequest command = new UpdateAccountRequest
            {
                AccountId = id,
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

        [HttpPatch]
        [Route("{id}/change-email")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateAccountEmail(int id, [FromBody] AccountEmailPatchDto updatedAccount)
        {
            UpdateAccountRequest command = new UpdateAccountRequest
            {
                AccountId = id,
                AccountEmail = updatedAccount.Email,
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
        [Route("{id}/change-password")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateAccountPassword(int id, [FromBody] AccountPasswordPatchDto updatedAccount)
        {
            UpdateAccountRequest command = new UpdateAccountRequest
            {
                AccountId = id,
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
        [Route("{id}/archive")]
        [Authorize]
        public async Task<IActionResult> ArchiveAccount(int id)
        {
            ArchiveAccountRequest command = new ArchiveAccountRequest { AccountId = id };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{accountId}/wishlists/{wishlistId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DeleteWishlistByAccount(int accountId, int wishlistId)
        {
            DeleteWishlistRequest command = new DeleteWishlistRequest
            {
                WishlistAccountId = accountId,
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
