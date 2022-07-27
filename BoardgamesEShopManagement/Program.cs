// See https://aka.ms/new-console-template for more information
using BoardgamesEShopManagement;

Console.WriteLine("Hello, World!\n");

var productExample = new Product { ProductId = 1, ProductImage = "base64imageexample", ProductName = "BoardgameExample", ProductDescription = "First awesome item example", ProductPrice = 10m };

Console.WriteLine(productExample.ToString(false));
Console.WriteLine(productExample.ToString(true, 34));
Console.WriteLine(productExample.ToString());

Shop shop = new Shop();

shop.Add(new Product { ProductId = 1, ProductImage = "base64image1", ProductName = "Boardgame1", ProductDescription = "First awesome item", ProductPrice= 10m});
shop.Add(new Product { ProductId = 2, ProductImage = "base64image2", ProductName = "Boardgame2", ProductDescription = "Second awesome item", ProductPrice = 20m });
shop.Add(new Product { ProductId = 3, ProductImage = "base64image3", ProductName = "Boardgame3", ProductDescription = "Third awesome item", ProductPrice = 30m });

Console.WriteLine("\nProducts are: ");
foreach (var product in shop)
    Console.WriteLine(product);

var originalProduct = new Product { ProductId = 4, ProductImage = "base64image4", ProductName = "Boardgame4", ProductDescription = "Forth awesome item", ProductPrice = 40m };
var clonedProduct = (Product)originalProduct.Clone();
shop.Add(originalProduct);
shop.Add(clonedProduct);

Console.WriteLine("Products with the 4th cloned (except id) are: ");
foreach (var product in shop)
    Console.WriteLine(product);