using MediatR;

using BoardgamesEShopManagement.ConsolePresentation;
using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Infrastructure;

Seeder.SeedData();

IMediator mediator = MediatorInitializer.Init();

Console.WriteLine("\nWelcome!");

while (true)
{
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1 Create boardgame");
    //Console.WriteLine("2 Create order");
    Console.WriteLine("3 Create wishlist");
    Console.WriteLine("4 Get address");
    Console.WriteLine("5 Get categories");
    Console.WriteLine("6 Get specified wishlist per account");
    Console.WriteLine("7 Get orders per account");
    Console.WriteLine("8 Update account");
    //Console.WriteLine("9 Delete review");
    Console.WriteLine("0 Exit");

    Int32 action = Convert.ToInt32(Console.ReadLine());

    switch (action)
    {
        case 1:
            Boardgame boardgame = await ConsoleInputs.AddBoardgame(mediator);
            ConsoleInputs.DisplayItem<Boardgame>(boardgame);
            break;
        /*case 2:
            Order order = await ConsoleInputs.AddOrder(mediator);
            ConsoleInputs.DisplayItem<Order>(order);
            break;
        case 3:
            Wishlist wishlist = await ConsoleInputs.AddWishlist(mediator);
            ConsoleInputs.DisplayItem<Wishlist>(wishlist);
            break;*/
        case 4:
            Address address = await ConsoleInputs.GetAddress(mediator);
            ConsoleInputs.DisplayItem<Address>(address);
            break;
        case 5:
            List<Category> categoriesList = await ConsoleInputs.GetCategoriesList(mediator);
            ConsoleInputs.DisplayItem<List<Category>>(categoriesList);
            break;
        case 6:
            Wishlist singleWishlist = await ConsoleInputs.GetWishlistByAccount(mediator);
            ConsoleInputs.DisplayItem<Wishlist>(singleWishlist);
            break;
        case 7:
            List<Order> ordersList = await ConsoleInputs.GetOrdersListPerAccount(mediator);
            ConsoleInputs.DisplayItem<List<Order>>(ordersList);
            break;
        case 8:
            Account account = await ConsoleInputs.UpdateAccount(mediator);
            ConsoleInputs.DisplayItem<Account>(account);
            break;
        /*case 9:
            bool isDeleted = await ConsoleInputs.DeleteReview(mediator);
            ConsoleInputs.DisplayItem<bool>(isDeleted);
            break;*/
        case 0:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine($"Invalid action: {action}");
            break;
    }
}
