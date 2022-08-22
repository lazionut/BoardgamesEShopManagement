using Microsoft.EntityFrameworkCore;

using BoardgamesEShopManagement.Infrastructure;

await using ShopContext context = new ShopContext();

Seeder.SeedData();

var maximumOrderedBoardgames = await context.OrderItems.GroupBy(orderItem => orderItem.BoardgameId)
    .Select(orderItemGroup => new
    {
        BoardgameId = orderItemGroup.Key,
        MaximumQuantity = orderItemGroup.Max(boardgame => boardgame.Quantity),
    })
    .ToListAsync();

maximumOrderedBoardgames.ForEach(orderItemGroup => Console.WriteLine("Boardgame with id "
    + orderItemGroup.BoardgameId
    + " it was maximum ordered of "
    + orderItemGroup.MaximumQuantity
    + " times "));
Console.WriteLine('\n');


var linkPerBoardgame = await context.Boardgames
        .GroupBy(boardgame => boardgame.Link)
        .Select(linkGroup => new
        {
            Link = linkGroup.Key,
            Count = linkGroup.Count()
        })
        .ToListAsync();

linkPerBoardgame.ForEach(linkGroup => Console.WriteLine(linkGroup.Link
    + " appears "
    + linkGroup.Count
    + " times"));
Console.WriteLine('\n');


var boardgamesThatCostsHigherThanAValue = await context.Boardgames
    .GroupBy(boardgame => new { boardgame.Name, boardgame.Price })
    .Select(priceGroup => new
    {
        Name = priceGroup.Key.Name,
        Price = priceGroup.Key.Price
    })
    .Where(priceGroup => priceGroup.Price > 500)
    .ToListAsync();

boardgamesThatCostsHigherThanAValue.ForEach(priceGroup => Console.WriteLine(priceGroup.Name
    + " that costs "
    + priceGroup.Price
    + ", so it has a price higher than 500"));
Console.WriteLine('\n');


var pricesOfBoardgamesWithLink = await context.Boardgames
    .GroupBy(boardgame => boardgame.Link)
    .Select(boardgameGroup => new
    {
        Link = boardgameGroup.Key,
        TotalPrice = boardgameGroup.Sum(boardgame => boardgame.Price)
    })
    .ToListAsync();

pricesOfBoardgamesWithLink.ForEach(boardgameGroup => Console.WriteLine(
    "Boardgames with link " + boardgameGroup.Link
    + " have a total costs of "
    + boardgameGroup.TotalPrice));
Console.WriteLine('\n');


var averageReviewScore = await context.Reviews
     .GroupBy(review => review.Boardgame.Name)
     .Select(reviewGroup => new
     {
         Name = reviewGroup.Key,
         AverageScore = reviewGroup.Average(review => review.Score),
         Count = reviewGroup.Count()
     })
.ToListAsync();

averageReviewScore.ForEach(review => Console.WriteLine("Boardgame "
    + review.Name
    + " has an average of "
    + review.AverageScore
    + " stars and "
    + review.Count
    + " reviews"));
Console.WriteLine('\n');


var minimumWishlishtedBoardgames = await context.WishlistItems.GroupBy(orderItem => orderItem.BoardgameId)
    .Select(wishlistItemGroup => new
    {
        BoardgameId = wishlistItemGroup.Key,
        MinimumQuantity = wishlistItemGroup.Max(boardgame => boardgame.Quantity),
    })
    .ToListAsync();

minimumWishlishtedBoardgames.ForEach(wishlistItemGroup => Console.WriteLine("Boardgame with id "
    + wishlistItemGroup.BoardgameId
    + " was minimum wishlisted of "
    + wishlistItemGroup.MinimumQuantity
    + " times "));
Console.WriteLine('\n');


var reviewsPerBoardgameWithPrefix = await context.Boardgames
                        .SelectMany(boardgame => boardgame.Reviews, (b, r) =>
                            new
                            {
                                b.Name,
                                r.Author,
                                r.Content
                            }
                        )
                        .Where(boardgame => boardgame.Name.StartsWith("P") || boardgame.Name.StartsWith("T"))
                        .ToListAsync();

reviewsPerBoardgameWithPrefix.ForEach(boardgameReviews => Console.WriteLine("Boardgame that starts with P or T "
    + boardgameReviews.Name
    + " has a review by "
    + boardgameReviews.Author
    + " which contains "
    + boardgameReviews.Content));
Console.WriteLine('\n');


var boardgamesOrderedMoreThanAValue = await context.Boardgames
                        .SelectMany(order => order.Orders, (b, o) =>
                        new
                        {
                            o.BuyerName,
                            b.Name,
                            o.Total
                        })
                        .Where(boardgameOrder => boardgameOrder.Total > 300)
                        .ToListAsync();

boardgamesOrderedMoreThanAValue.ForEach(boardgameReviews => Console.WriteLine(
      boardgameReviews.BuyerName
    + " bought "
    + boardgameReviews.Name
    + " with a total value of higher than 300"));
Console.WriteLine('\n');


var wishlistsNumberPerBoardgames = await context.Boardgames
    .SelectMany(boardgame => boardgame.Wishlists, (b, w) =>
    new
    {
        b.Name,
        w.Boardgames.Count
    })
    .ToListAsync();

wishlistsNumberPerBoardgames.ForEach(boardgameWishlist => Console.WriteLine("Boardgame "
     + boardgameWishlist.Name
     + " was wishlisted "
     + boardgameWishlist.Count
     + " times"));
Console.WriteLine('\n');


var moreThanAValueOrderedBoardgames = await context.Boardgames
    .SelectMany(boardgame => boardgame.Orders, (b, o) =>
    new
    {
        b.Name,
        o.Boardgames.Count,
    })
    .Where(boardgameOrder => boardgameOrder.Count > 2)
    .ToListAsync();

moreThanAValueOrderedBoardgames.ForEach(boardgameOrder => Console.WriteLine("Boardgame "
     + boardgameOrder.Name
     + " was ordered more than 2 times"));
Console.WriteLine('\n');