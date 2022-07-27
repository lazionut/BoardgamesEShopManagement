using BoardgamesEShopManagement.Domain;

/*
Boardgame myBoardgame1 = new Boardgame { BoardgameId = 1, BoardgameImage = "base64imageexample1", BoardgameName = "BoardgameExample", BoardgameDescription = "My first boardgame", BoardgamePrice = 20m };
Boardgame myBoardgame2 = new Boardgame { BoardgameId = 2, BoardgameImage = "base64imageexample2", BoardgameName = "BoardgameExample", BoardgameDescription = "My second boardgame", BoardgamePrice = 45m };
Boardgame myBoardgame3 = new Boardgame { BoardgameId = 3, BoardgameImage = "base64imageexample3", BoardgameName = "BoardgameExample", BoardgameDescription = "My third boardgame", BoardgamePrice = 56m };

Boardgame.AddBoardgame(myBoardgame1);
Boardgame.AddBoardgame(myBoardgame2);
Boardgame.AddBoardgame(myBoardgame3);

Boardgame.GetBoardgame(2);
Console.WriteLine('\n');

Boardgame.ChangeBoardgame(1);
Boardgame.GetBoardgames();
Console.WriteLine('\n');

Boardgame.RemoveBoardgame(0);
Boardgame.RemoveBoardgame(3);
Boardgame.GetBoardgames();
*/

using ExceptionDispatchInfo = System.Runtime.ExceptionServices.ExceptionDispatchInfo;
using System.Diagnostics;

List<string> boardgamesList = new List<string> { "boardgame1", "boardgame2", "boardgame3" };

//method which checks input argument and throws exception
void AddBoardgame(string boardgame)
{
    if (boardgame == null)
        throw new ArgumentNullException("Boardgame is null");

    boardgamesList.Add(boardgame);
}

//AddBoardgame(null);

decimal DivideTwoPricesWithCustomException(decimal a, decimal b)
{
    if (b == 0m)
        //throwing custom exception
        throw new CustomException();

    return a / b;
}

//DivideTwoPricesWithCustomException(1, 0);

decimal DivideTwoPrices(decimal a, decimal b)
{
    return a / b;
}

//try-catch-finally with multiple catch statements
try
{
    var result = DivideTwoPrices(1m, 0m);
}
catch (CustomException exception)
{
    throw new CustomException();
}
catch (ArithmeticException exception)
{
    //debug class
    Debug.Fail("exception occured");
    Console.WriteLine("Cannot divide by 0!");
}
finally
{
    Console.WriteLine("Always throws error");
}

//rethrow exception
try
{
    Console.WriteLine("Wait for a task");
}
catch (AggregateException ex)
{
    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
}

//custom exception
public class CustomException : Exception
{
}

//conditional compilation symbol
#if DEBUG
public class TestingException : Exception
{

}
#endif