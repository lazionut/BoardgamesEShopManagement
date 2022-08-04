using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Infrastructure.Repositories;

Boardgame myBoardgame1 = new Boardgame { Id = 1, BoardgameImage = "base64imageexample1", BoardgameName = "BoardgameExample1", BoardgameDescription = "My first boardgame", BoardgamePrice = 20m };
Boardgame myBoardgame2 = new Boardgame { Id = 2, BoardgameImage = "base64imageexample2", BoardgameName = "BoardgameExample2", BoardgameDescription = "My second boardgame", BoardgamePrice = 45m };
Boardgame myBoardgame3 = new Boardgame { Id = 3, BoardgameImage = "base64imageexample3", BoardgameName = "BoardgameExample3", BoardgameDescription = "My third boardgame", BoardgamePrice = 56m };

BoardgameRepository boardgameRepository = new BoardgameRepository();

boardgameRepository.Create(myBoardgame1);
boardgameRepository.Create(myBoardgame2);
boardgameRepository.Create(myBoardgame3);
boardgameRepository.GetAll();

//boardgameRepository.GetBoardgame(-5);
Console.WriteLine('\n');

boardgameRepository.Update(1);
boardgameRepository.GetAll();
Console.WriteLine('\n');

boardgameRepository.Delete(1);
boardgameRepository.Delete(3);
boardgameRepository.GetAll();