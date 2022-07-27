using BoardgamesEShopManagement.Domain.Models;
using BoardgamesEShopManagement.DAL.Repository;

Boardgame myBoardgame1 = new Boardgame { BoardgameId = 1, BoardgameImage = "base64imageexample1", BoardgameName = "BoardgameExample", BoardgameDescription = "My first boardgame", BoardgamePrice = 20m };
Boardgame myBoardgame2 = new Boardgame { BoardgameId = 2, BoardgameImage = "base64imageexample2", BoardgameName = "BoardgameExample", BoardgameDescription = "My second boardgame", BoardgamePrice = 45m };
Boardgame myBoardgame3 = new Boardgame { BoardgameId = 3, BoardgameImage = "base64imageexample3", BoardgameName = "BoardgameExample", BoardgameDescription = "My third boardgame", BoardgamePrice = 56m };

BoardgameRepository.AddBoardgame(myBoardgame1);
BoardgameRepository.AddBoardgame(myBoardgame2);
BoardgameRepository.AddBoardgame(myBoardgame3);
BoardgameRepository.GetBoardgames();

BoardgameRepository.GetBoardgame(-5);
Console.WriteLine('\n');

BoardgameRepository.ChangeBoardgame(1);
BoardgameRepository.GetBoardgames();
Console.WriteLine('\n');

BoardgameRepository.RemoveBoardgame(0);
BoardgameRepository.RemoveBoardgame(3);
BoardgameRepository.GetBoardgames();