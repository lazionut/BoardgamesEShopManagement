using MediatR;
using Microsoft.Extensions.DependencyInjection;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Boardgames.Commands.CreateBoardgame;
using BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;
using BoardgamesEShopManagement.Infrastructure.Repositories;

/*
Boardgame myBoardgame1 = new Boardgame { Id = 1, BoardgameImage = "base64imageexample1", BoardgameName = "BoardgameExample1", BoardgameDescription = "My first boardgame", BoardgamePrice = 20m };
Boardgame myBoardgame2 = new Boardgame { Id = 2, BoardgameImage = "base64imageexample2", BoardgameName = "BoardgameExample2", BoardgameDescription = "My second boardgame", BoardgamePrice = 45m };
Boardgame myBoardgame3 = new Boardgame { Id = 3, BoardgameImage = "base64imageexample3", BoardgameName = "BoardgameExample3", BoardgameDescription = "My third boardgame", BoardgamePrice = 56m };

BoardgameRepository boardgameRepository = new BoardgameRepository();

boardgameRepository.Create(myBoardgame1);
boardgameRepository.Create(myBoardgame2);
boardgameRepository.Create(myBoardgame3);

boardgameRepository
    .GetAll()
    .ToList()
    .ForEach(boardgame => Console.WriteLine(boardgame));

//boardgameRepository.GetBoardgame(-5);
Console.WriteLine('\n');

boardgameRepository.Update(1);
boardgameRepository
    .GetAll()
    .ToList()
    .ForEach(boardgame => Console.WriteLine(boardgame));
Console.WriteLine('\n');

boardgameRepository.Delete(1);
boardgameRepository.Delete(3);
boardgameRepository
    .GetAll()
    .ToList()
    .ForEach(boardgame => Console.WriteLine(boardgame));
*/

var diContainer = new ServiceCollection()
    .AddScoped<IBoardgameRepository, BoardgameRepository>()
    .AddMediatR(typeof(IBoardgameRepository))
    .BuildServiceProvider();

var mediator = diContainer.GetRequiredService<IMediator>();

var firstBoardgameId = await mediator.Send(new CreateBoardgameRequest { BoardgameId = 1, BoardgameImage = "base64image1", BoardgameName = "BoardgameName", BoardgameDescription = "My first boardgame description", BoardgamePrice = 20m });
await mediator.Send(new CreateBoardgameRequest { BoardgameId = 2, BoardgameImage = "base64image2", BoardgameName = "BoardgameName2", BoardgameDescription = "My second boardgame description", BoardgamePrice = 45m });
await mediator.Send(new CreateBoardgameRequest { BoardgameId = 3, BoardgameImage = "base64image3", BoardgameName = "BoardgameName3", BoardgameDescription = "My third boardgame description", BoardgamePrice = 35m });
var boardgames = await mediator.Send(new GetBoardgamesListQuery());

var thirdBoardgamePrice = boardgames.FirstOrDefault(boardgame => boardgame.Id == 3)?.Price;

Console.WriteLine(firstBoardgameId);
Console.WriteLine(thirdBoardgamePrice);