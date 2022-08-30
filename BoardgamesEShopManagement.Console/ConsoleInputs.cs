using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame;
using BoardgamesEShopManagement.Application.Addresses.Queries.GetAddress;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList;
using BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount;
using BoardgamesEShopManagement.Application.Orders.Commands.CreateOrder;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist;
using BoardgamesEShopManagement.Application.Reviews.Commands.DeleteReview;
using BoardgamesEShopManagement.Application.Categories.Queries.GetOrdersListPerAccount;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistByAccount;

namespace BoardgamesEShopManagement.ConsolePresentation
{
    internal static class ConsoleInputs
    {
        internal static void DisplayItem<T>(T item)
        {
            var serializedProduct = JsonConvert.SerializeObject(item, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented,
            });

            Console.WriteLine(serializedProduct);
            Console.WriteLine();
        }

        internal static async Task<Boardgame> AddBoardgame(IMediator mediator)
        {
            CreateBoardgameRequest addBoardgameCommand = new CreateBoardgameRequest();

            Console.WriteLine($"Insert {nameof(addBoardgameCommand.BoardgameImage)}:");
            addBoardgameCommand.BoardgameImage = Console.ReadLine();

            Console.WriteLine($"Insert {nameof(addBoardgameCommand.BoardgameName)}:");
            addBoardgameCommand.BoardgameName = Console.ReadLine();

            Console.WriteLine($"Insert {nameof(addBoardgameCommand.BoardgameDescription)}:");
            addBoardgameCommand.BoardgameDescription = Console.ReadLine();

            Console.WriteLine($"Insert {nameof(addBoardgameCommand.BoardgameLink)}:");
            addBoardgameCommand.BoardgameLink = Console.ReadLine();

            Console.WriteLine($"Insert {nameof(addBoardgameCommand.BoardgamePrice)}:");
            addBoardgameCommand.BoardgamePrice = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine($"Insert {nameof(addBoardgameCommand.BoardgameCategoryId)}:");
            addBoardgameCommand.BoardgameCategoryId = Convert.ToInt32(Console.ReadLine());

            return await mediator.Send(addBoardgameCommand);
        }

        /*
        internal static async Task<Order> AddOrder(IMediator mediator)
        {
            CreateOrderItemRequest addOrderCommand = new CreateOrderItemRequest();

            Console.WriteLine($"Insert {nameof(addOrderCommand.Total)}:");
            addOrderCommand.Total = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine($"Insert {nameof(addOrderCommand.AccountId)}:");
            addOrderCommand.AccountId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Insert {nameof(addOrderCommand.BoardgameId)}:");
            addOrderCommand.BoardgameId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Insert {nameof(addOrderCommand.OrderId)}:");
            addOrderCommand.OrderId = Convert.ToInt32(Console.ReadLine());

            return await mediator.Send(addOrderCommand);
        }
        */

        /*
        internal static async Task<Wishlist> AddWishlist(IMediator mediator)
        {
            CreateWishlistRequest addWishlistCommand = new CreateWishlistRequest();

            Console.WriteLine($"Insert {nameof(addWishlistCommand.AccountId)}:");
            addWishlistCommand.AccountId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Insert {nameof(addWishlistCommand.BoardgameId)}:");
            addWishlistCommand.BoardgameId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Insert {nameof(addWishlistCommand.WishlistId)}:");
            addWishlistCommand.WishlistId = Convert.ToInt32(Console.ReadLine());

            return await mediator.Send(addWishlistCommand);
        }
        */

        internal static async Task<Address> GetAddress(IMediator mediator)
        {
            GetAddressQuery getAddressQuery = new GetAddressQuery();
            Console.WriteLine($"Insert {nameof(getAddressQuery.AddressId)}");
            getAddressQuery.AddressId = Convert.ToInt32(Console.ReadLine());

            return await mediator.Send(getAddressQuery);
        }

        internal static async Task<Wishlist> GetWishlistByAccount(IMediator mediator)
        {
            GetWishlistByAccountQuery getWishlistByAccountAccountQuery = new GetWishlistByAccountQuery();

            Console.WriteLine($"Insert {nameof(getWishlistByAccountAccountQuery.AccountId)}");
            getWishlistByAccountAccountQuery.AccountId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Insert {nameof(getWishlistByAccountAccountQuery.WishlistId)}");
            getWishlistByAccountAccountQuery.WishlistId = Convert.ToInt32(Console.ReadLine());

            return await mediator.Send(getWishlistByAccountAccountQuery);
        }

        internal static async Task<List<Order>> GetOrdersListPerAccount(IMediator mediator)
        {
            GetOrdersListPerAccountQuery getOrdersPerAccountQuery = new GetOrdersListPerAccountQuery();
            Console.WriteLine($"Insert {nameof(getOrdersPerAccountQuery.AccountId)}");
            getOrdersPerAccountQuery.AccountId = Convert.ToInt32(Console.ReadLine());

            return await mediator.Send(getOrdersPerAccountQuery);
        }

        internal static async Task<List<Category>> GetCategoriesList(IMediator mediator)
        {
            GetCategoriesListQuery getCategoriesListQuery = new GetCategoriesListQuery();

            return await mediator.Send(getCategoriesListQuery);
        }

        internal static async Task<Account> UpdateAccount(IMediator mediator)
        {
            UpdateAccountRequest updateAccountCommand = new UpdateAccountRequest();

            Account account = new Account();

            Console.WriteLine($"Insert {nameof(updateAccountCommand.AccountId)}:");
            updateAccountCommand.AccountId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Insert {nameof(account.FirstName)}:");
            updateAccountCommand.AccountFirstName = Console.ReadLine();

            Console.WriteLine($"Insert {nameof(account.LastName)}:");
            updateAccountCommand.AccountLastName = Console.ReadLine();

            Console.WriteLine($"Insert {nameof(account.Email)}:");
            updateAccountCommand.AccountEmail = Console.ReadLine();

            Console.WriteLine($"Insert {nameof(account.Password)}:");
            updateAccountCommand.AccountPassword = Console.ReadLine();

            return await mediator.Send(updateAccountCommand);
        }

        /*
        internal static async Task<bool> DeleteReview(IMediator mediator)
        {
            DeleteReviewRequest deleteReviewCommand = new DeleteReviewRequest();

            Console.WriteLine($"Insert {nameof(deleteReviewCommand.ReviewId)}:");
            deleteReviewCommand.ReviewId = Convert.ToInt32(Console.ReadLine());

            return await mediator.Send(deleteReviewCommand);
        }
        */
    }
}
