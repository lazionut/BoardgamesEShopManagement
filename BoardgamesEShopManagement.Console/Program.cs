using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Infrastructure;

await using ShopContext context = new ShopContext();

Seeder.SeedData();

var boardgames = await context.Boardgames.GroupBy(boardgame => boardgame.Name)
    .Select(boardgame => new
    {
        Name = boardgame.Key,
        Price = boardgame.Sum(boardgame => boardgame.Price),
        Count = boardgame.Count()
    })
    .ToListAsync();

boardgames.ForEach(boardgame => Console.WriteLine(boardgame.Name + " costs " + boardgame.Price + " : " + boardgame.Count));