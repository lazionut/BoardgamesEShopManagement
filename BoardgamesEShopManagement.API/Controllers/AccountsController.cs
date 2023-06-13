using AutoMapper;
using BoardgamesEShopManagement.API.Controllers;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Accounts.Commands.AddRoleToAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.ArchiveAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsListCounter;
using BoardgamesEShopManagement.Application.Accounts.Queries.LoginAccount;
using BoardgamesEShopManagement.Application.Addresses.Commands.ArchiveAddress;
using BoardgamesEShopManagement.Application.Addresses.Commands.CreateAddress;
using BoardgamesEShopManagement.Application.Addresses.Commands.DeleteAddress;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrderByAccount;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListPerAccount;
using BoardgamesEShopManagement.Application.Orders.Queries.GetOrdersListPerAccountCounter;
using BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlistItem;
using BoardgamesEShopManagement.Application.Wishlists.Commands.UpdateWishlist;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistsListPerAccount;
using BoardgamesEShopManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountsController : CustomControllerBase
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
            GetAccountsListQuery queryAccounts = new GetAccountsListQuery
            {
                AccountPageIndex = pageIndex,
                AccountPageSize = pageSize
            };

            List<Account>? resultAccounts = await _mediator.Send(queryAccounts);

            if (resultAccounts == null)
            {
                return NotFound();
            }

            List<AccountAdminGetDto> mappedResultAccounts = _mapper.Map<List<AccountAdminGetDto>>(resultAccounts);

            GetAccountsListCounterQuery commandAccountsCounter = new GetAccountsListCounterQuery { };

            int resultAccountsCounter = await _mediator.Send(commandAccountsCounter);

            if (resultAccountsCounter == 0)
            {
                return NotFound();
            }

            int mappedResultAccountsCounter = mappedResultAccounts.Count();

            if (mappedResultAccountsCounter == 0)
            {
                return NotFound();
            }

            int pageCounter = resultAccountsCounter / mappedResultAccountsCounter;

            if (resultAccountsCounter % pageSize > 0)
            {
                ++pageCounter;
            }

            if (mappedResultAccountsCounter < pageSize)
            {
                pageCounter = pageIndex;
            }

            return Ok(new
            {
                pageCount = pageCounter,
                accounts = mappedResultAccounts
            });
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetAccount()
        {
            GetAccountQuery query = new GetAccountQuery { AccountId = GetAccountId() };

            Account? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            AccountGetDto mappedResult = _mapper.Map<AccountGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("orders/{id}")]
        public async Task<IActionResult> GetOrderByAccount(int id)
        {
            GetOrderByAccountQuery query = new GetOrderByAccountQuery { OrderAccountId = GetAccountId(), OrderId = id };

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
            GetOrdersListPerAccountQuery queryOrders = new GetOrdersListPerAccountQuery
            {
                OrderAccountId = GetAccountId(),
                OrderPageIndex = pageIndex,
                OrderPageSize = pageSize
            };

            List<Order>? resultOrders = await _mediator.Send(queryOrders);

            if (resultOrders == null)
            {
                return NotFound();
            }

            List<OrderGetDto> mappedResultOrders = _mapper.Map<List<OrderGetDto>>(resultOrders);

            GetOrdersListPerAccountCounterQuery commandOrdersCounter = new GetOrdersListPerAccountCounterQuery { OrderAccountId = GetAccountId() };

            int resultOrdersCounter = await _mediator.Send(commandOrdersCounter);

            if (resultOrdersCounter == 0)
            {
                return NotFound();
            }

            int mappedResultOrdersCounter = mappedResultOrders.Count();

            if (mappedResultOrdersCounter == 0)
            {
                return NotFound();
            }

            int pageCounter = resultOrdersCounter / mappedResultOrdersCounter;

            if (resultOrdersCounter % pageSize > 0)
            {
                ++pageCounter;
            }

            if (mappedResultOrdersCounter < pageSize)
            {
                pageCounter = pageIndex;
            }

            return Ok(new
            {
                pageCount = pageCounter,
                orders = mappedResultOrders
            });
        }

        [HttpGet]
        [Route("wishlists")]
        public async Task<IActionResult> GetWishlistsPerAccount()
        {
            GetWishlistsListPerAccountQuery query = new GetWishlistsListPerAccountQuery
            {
                WishlistAccountId = GetAccountId(),
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
        public async Task<IActionResult> UpdateAccount([FromBody] AccountPatchDto updatedAccount)
        {
            UpdateAccountRequest command = new UpdateAccountRequest
            {
                AccountId = GetAccountId(),
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

        [HttpPut]
        [Route("wishlists/{id}")]
        public async Task<IActionResult> UpdateWishlist(int id, [FromBody] WishlistPutDto wishlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UpdateWishlistRequest wishlistCommand = new UpdateWishlistRequest
            {
                WishlistId = id,
                WishlistName = wishlist.Name,
                WishlistAccountId = GetAccountId(),
                WishlistBoardgameIds = wishlist.BoardgameIds
            };

            Wishlist? wishlistResult = await _mediator.Send(wishlistCommand);

            if (wishlistResult == null)
            {
                return NotFound();
            }

            var wishlistResultBoardgameIds = new List<int>();

            foreach (Boardgame boardgame in wishlistResult.Boardgames)
            {
                wishlistResultBoardgameIds.Add(boardgame.Id);
            }

            List<int> toBeRemovedBoardgames = wishlistResultBoardgameIds.Except(wishlist.BoardgameIds).ToList();

            foreach (int boardgameId in toBeRemovedBoardgames)
            {
                DeleteWishlistItemRequest wishlistItemCommand = new DeleteWishlistItemRequest
                {
                    WishlistId = id,
                    BoardgameId = boardgameId,
                };

                await _mediator.Send(wishlistItemCommand);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            DeleteAccountRequest commandAccount = new DeleteAccountRequest { AccountId = id };

            Account? resultAccount = await _mediator.Send(commandAccount);

            if (resultAccount == null)
            {
                return NotFound();
            }

            DeleteAddressRequest commandAddress = new DeleteAddressRequest { AddressId = resultAccount.AddressId };

            Address? resultAddress = await _mediator.Send(commandAddress);

            if (resultAddress == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("archive")]
        public async Task<IActionResult> ArchiveAccount()
        {
            ArchiveAccountRequest accountCommand = new ArchiveAccountRequest { AccountId = GetAccountId() };

            Account? accountResult = await _mediator.Send(accountCommand);

            if (accountResult == null)
            {
                return NotFound();
            }

            ArchiveAddressRequest addressCommand = new ArchiveAddressRequest { AddressId = accountResult.AddressId };

            Address? result = await _mediator.Send(addressCommand);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("wishlists/{id}")]
        public async Task<IActionResult> DeleteWishlistByAccount(int id)
        {
            DeleteWishlistRequest command = new DeleteWishlistRequest
            {
                WishlistAccountId = GetAccountId(),
                WishlistId = id
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