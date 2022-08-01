using BoardgamesEShopManagement.Domain;
using BoardgamesEShopManagement.Domain.Models;
using BoardgamesEShopManagement.DAL.Repository;

/*
Boardgame myBoardgame1 = new Boardgame { BoardgameId = 1, BoardgameImage = "base64imageexample1", BoardgameName = "BoardgameExample1", BoardgameDescription = "My first boardgame", BoardgamePrice = 20m };
Boardgame myBoardgame2 = new Boardgame { BoardgameId = 2, BoardgameImage = "base64imageexample2", BoardgameName = "BoardgameExample2", BoardgameDescription = "My second boardgame", BoardgamePrice = 45m };
Boardgame myBoardgame3 = new Boardgame { BoardgameId = 3, BoardgameImage = "base64imageexample3", BoardgameName = "BoardgameExample3", BoardgameDescription = "My third boardgame", BoardgamePrice = 56m };

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
*/

/*
List<Boardgame> boardgamesList = new List<Boardgame> { myBoardgame1, myBoardgame2, myBoardgame3 };

string fileName = "BoardgamesNames.txt";
string path = Path.Combine(Environment.CurrentDirectory, fileName);
BoardgameWriter.writeBoardgamesNames(path, boardgamesList);
BoardgameReader.readBoardgamesNames(path);
*/

List<Boardgame> boardgamesList = new List<Boardgame>();

boardgamesList.Add(new Boardgame { BoardgameId = 1, BoardgameImage = "base64imageexample1", BoardgameName = "BoardgameExample1", BoardgameDescription = "My first boardgame", BoardgamePrice = 20m });
boardgamesList.Add(new Boardgame { BoardgameId = 2, BoardgameImage = "base64imageexample2", BoardgameName = "BoardgameExample2", BoardgameDescription = "My second boardgame", BoardgamePrice = 45m });
boardgamesList.Add(new Boardgame { BoardgameId = 3, BoardgameImage = "base64imageexample3", BoardgameName = "BoardgameExample3", BoardgameDescription = "My third boardgame", BoardgamePrice = 56m });

IEnumerable<Boardgame> filteredBoardgames = boardgamesList
    .Where(boardgame => boardgame.BoardgamePrice > 30m);

foreach (Boardgame boardgame in filteredBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IEnumerable<Boardgame> firstFilteredBoardgame = boardgamesList.Take(1);

foreach (Boardgame boardgame in firstFilteredBoardgame)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IEnumerable<Boardgame> lastFilteredBoardgame = boardgamesList.Skip(2);

foreach (Boardgame boardgame in lastFilteredBoardgame)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IEnumerable<Boardgame> conditionFilteredBoardgames = boardgamesList
    .TakeWhile(boardgame => boardgame.BoardgamePrice > 10m);

foreach (Boardgame boardgame in conditionFilteredBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IEnumerable<Boardgame> skippedFilteredBoardgames = boardgamesList
    .SkipWhile(boardgame => boardgame.BoardgamePrice < 30m);

foreach (Boardgame boardgame in skippedFilteredBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IEnumerable<Boardgame> distinctBoardgames = boardgamesList.Distinct();

foreach (Boardgame boardgame in distinctBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


var newBoardgames = boardgamesList.Select((boardgame, index) =>
                               new { index, boardgame = boardgame });

foreach (var boardgame in newBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IEnumerable<Boardgame> orderedBoardgames = boardgamesList
    .OrderByDescending(boardgame => boardgame.BoardgamePrice);

foreach (Boardgame boardgame in orderedBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IEnumerable<Boardgame> thenOrderedBoardgames = boardgamesList
    .OrderBy(boardgame => boardgame.BoardgameDescription)
    .ThenByDescending(boardgame => boardgame.BoardgameName);

foreach (Boardgame boardgame in thenOrderedBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IQueryable<Boardgame> reversedBoardgames = boardgamesList.AsQueryable().Reverse();

foreach (Boardgame boardgame in reversedBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IQueryable<IGrouping<string, Boardgame>> groupedBoardgames = boardgamesList
    .AsQueryable()
    .GroupBy(boardgame => boardgame.BoardgameName);

foreach (IGrouping<string, Boardgame> boardgameName in groupedBoardgames)
{
    Console.WriteLine(boardgameName.Key);
}
Console.WriteLine('\n');


Boardgame[] boardgamesArray = boardgamesList.ToArray();

foreach (Boardgame boardgame in boardgamesArray)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IDictionary<int, Boardgame> boardgameDictionary = boardgamesList
    .ToDictionary(boardgame => boardgame.BoardgameId);

foreach (KeyValuePair<int, Boardgame> boardgame in boardgameDictionary)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


ILookup<string, Boardgame> boardgameLookup = boardgamesList
    .ToLookup(boardgame => boardgame.BoardgameDescription);

foreach (IGrouping<string, Boardgame> boardgameDescription in boardgameLookup)
{
    Console.WriteLine(boardgameDescription.Key);
}
Console.WriteLine('\n');


IEnumerable<Boardgame> boardgamesEnumerable = boardgamesList
    .Where(boardgame => boardgame.BoardgameName.Contains("o"));

foreach (Boardgame boardgame in boardgamesEnumerable)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IQueryable<Boardgame> boardgamesQueryable = boardgamesList
    .AsQueryable()
    .Where(boardgame => boardgame.BoardgameDescription.StartsWith("My second"));

foreach (Boardgame boardgame in boardgamesQueryable)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


Boardgame firstBoardgame = boardgamesList.First();

Console.WriteLine(firstBoardgame);
Console.WriteLine('\n');


Boardgame lastBoardgame = boardgamesList.Last();

Console.WriteLine(lastBoardgame);
Console.WriteLine('\n');

/*
Boardgame singleBoardgame = boardgamesList.Single();

Console.WriteLine(singleBoardgame);
Console.WriteLine('\n');
*/


Boardgame secondBoardgame = boardgamesList.ElementAt(1);

Console.WriteLine(secondBoardgame);
Console.WriteLine('\n');


List<Boardgame> emptyBoardgamesList = new List<Boardgame>();
IEnumerable<Boardgame> defaultBoardgame = emptyBoardgamesList.DefaultIfEmpty();

foreach (Boardgame boardgame in defaultBoardgame)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


int boardgamesNumber = boardgamesList.Count();

Console.WriteLine(boardgamesNumber);
Console.WriteLine('\n');


Boardgame cheapestBoardgame = boardgamesList.Min();

Console.WriteLine(cheapestBoardgame);
Console.WriteLine('\n');

Boardgame mostExpensiveBoardgame = boardgamesList.Max();

Console.WriteLine(mostExpensiveBoardgame);
Console.WriteLine('\n');


decimal averagePriceBoardgame = boardgamesList
    .Average(boardgame => boardgame.BoardgamePrice);

Console.WriteLine(averagePriceBoardgame);
Console.WriteLine('\n');


var totalPriceBoardgames = boardgamesList
    .GroupBy(boardgame => boardgame.BoardgameName)
    .Select(boardgame => new { name = boardgame.Key, val = boardgame.Sum(boardgame => boardgame.BoardgamePrice) });

foreach (var boardgame in totalPriceBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IEnumerable<Boardgame> existingBoardgames = boardgamesList
    .Where(boardgame => boardgame.BoardgameName.Any());

foreach (Boardgame boardgame in existingBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


List<Boardgame> secondBoardgamesList = boardgamesList;
secondBoardgamesList.Add(new Boardgame { BoardgameId = 2, BoardgameImage = "base64imageexample2", BoardgameName = "BoardgameExample2", BoardgameDescription = "My second boardgame", BoardgamePrice = 45m });

bool equalBoardgamesList = boardgamesList.SequenceEqual(secondBoardgamesList);

Console.WriteLine(equalBoardgamesList);
Console.WriteLine('\n');


var withDescriptionBoardgames = boardgamesList
    .All(boardgame => boardgame.BoardgameDescription.StartsWith("T"));

Console.WriteLine(withDescriptionBoardgames);
Console.WriteLine('\n');


Boardgame repeatedBoardgame = new Boardgame { BoardgameId = 1, BoardgameImage = "base64imageexample1", BoardgameName = "BoardgameExample1", BoardgameDescription = "My first boardgame", BoardgamePrice = 20m };
IEnumerable<Boardgame> repeatedBoardgames = Enumerable
    .Repeat(repeatedBoardgame, 3);

foreach (Boardgame boardgame in repeatedBoardgames)
{
    Console.WriteLine(boardgame);
}
Console.WriteLine('\n');


IEnumerable<int> tenIdsBoardgame = Enumerable.Range(1, 10);

foreach (int id in tenIdsBoardgame)
{
    Console.WriteLine(id);
}