using MediatR;
using Microsoft.Extensions.DependencyInjection;

using BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Infrastructure.Repositories;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistList;
using BoardgamesEShopManagement.Application.Wishlists.Commands.CreateWishlist;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.UpdateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Commands.DeleteBoardgame;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Reviews.Commands.CreateReview;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsList;

var diContainer = new ServiceCollection()
    .AddMediatR(typeof(IBoardgameRepository))
    .AddScoped<IBoardgameRepository, BoardgameRepository>()
    .AddScoped<IWishlistRepository, WishlistRepository>()
    .AddScoped<IReviewRepository, ReviewRepository>()
    .BuildServiceProvider();

var mediator = diContainer.GetRequiredService<IMediator>();

var firstBoardgameId = await mediator.Send(new CreateBoardgameRequest { BoardgameImage = "base64image1", BoardgameName = "BoardgameName", BoardgameDescription = "My first boardgame description", BoardgamePrice = 20m });
await mediator.Send(new CreateBoardgameRequest { BoardgameImage = "base64image2", BoardgameName = "BoardgameName2", BoardgameDescription = "My second boardgame description", BoardgamePrice = 45m });
await mediator.Send(new CreateBoardgameRequest { BoardgameImage = "base64image3", BoardgameName = "BoardgameName3", BoardgameDescription = "My third boardgame description", BoardgamePrice = 35m });
var boardgames = await mediator.Send(new GetBoardgamesListQuery());

var thirdBoardgamePrice = boardgames.FirstOrDefault(boardgame => boardgame.Id == 3)?.BoardgamePrice;
var firstBoardgame = await mediator.Send(new GetBoardgameQuery { BoardgameId = 1 });
var secondBoardgame = await mediator.Send(new GetBoardgameQuery { BoardgameId = 2 });
Console.WriteLine("Current second boardgame is: " + secondBoardgame);
await mediator.Send(new UpdateBoardgameRequest { BoardgameId = 2, Boardgame = new Boardgame { BoardgameImage = "base64image2", BoardgameName = "BoardgameName5", BoardgamePrice = 13m } });
secondBoardgame = await mediator.Send(new GetBoardgameQuery { BoardgameId = 2 });
Console.WriteLine("Updated second boardgame is: " + secondBoardgame);
var isThirdBoardgameDeleted = await mediator.Send(new DeleteBoardgameRequest { BoardgameId = 3 });

Console.WriteLine(firstBoardgameId);
Console.WriteLine(thirdBoardgamePrice);
Console.WriteLine(firstBoardgame);
Console.WriteLine(isThirdBoardgameDeleted);


await mediator.Send(new CreateWishlistRequest
{
    WishlistName = "MyWishlist1",
    WishlistItems = new List<WishlistItemDto>
                {
                    new() { Quantity = 1, BoardgameName = "Boardgame1" },
                    new() { Quantity = 2, BoardgameName = "Boardgame2" }
                }
});
await mediator.Send(new CreateWishlistRequest
{
    WishlistName = "MyWishlist2",
    WishlistItems = new List<WishlistItemDto>
                {
                    new() { Quantity = 1, BoardgameName = "Boardgame1" },
                    new() { Quantity = 2, BoardgameName = "Boardgame2" }
                }
});
var wishlistId = await mediator.Send(new CreateWishlistRequest
{
    WishlistName = "MyWishlist3",
    WishlistItems = new List<WishlistItemDto>
                {
                    new() { Quantity = 1, BoardgameName = "Boardgame1" },
                    new() { Quantity = 2, BoardgameName = "Boardgame2" }
                }
});

var wishlists = await mediator.Send(new GetWishlistsListQuery());


Console.WriteLine($"Order created with {wishlistId}");
var secondWishlistName = wishlists.FirstOrDefault(wishlist => wishlist.WishlistId == 2)?.WishlistName;
Console.WriteLine(secondWishlistName);

await mediator.Send(new CreateReviewRequest { BoardgameId = 1, ReviewTitle = "This is awesome", ReviewAuthor = "MyAuthor1", ReviewContent = "My review about this boardgame!" });
var secondReviewId = await mediator.Send(new CreateReviewRequest { BoardgameId = 1, ReviewTitle = "This is awesome", ReviewAuthor = "MyAuthor2", ReviewContent = "My review about this boardgame!" });
await mediator.Send(new CreateReviewRequest { BoardgameId = 1, ReviewTitle = "This is awesome", ReviewAuthor = "MyAuthor3", ReviewContent = "My review about this boardgame!" });
var reviews = await mediator.Send(new GetReviewsListQuery());

var secondReviewAuthor = reviews.FirstOrDefault(review => review.ReviewId == 2)?.ReviewAuthor;

Console.WriteLine(secondReviewId);
Console.WriteLine(secondReviewAuthor);