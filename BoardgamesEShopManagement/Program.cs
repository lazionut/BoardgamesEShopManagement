// See https://aka.ms/new-console-template for more information
using BoardgamesEShopManagement;

/*
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
*/

GenericArray<Product> productsArray = new GenericArray<Product>(3);

productsArray.AddElement(new Product { ProductId = 1, ProductImage = "base64image1", ProductName = "Boardgame1", ProductDescription = "First awesome item", ProductPrice = 10m });
productsArray.AddElement(new Product { ProductId = 2, ProductImage = "base64image2", ProductName = "Boardgame2", ProductDescription = "Second awesome item", ProductPrice = 20m });
productsArray.AddElement(new Product { ProductId = 3, ProductImage = "base64image3", ProductName = "Boardgame3", ProductDescription = "Third awesome item", ProductPrice = 30m });

Console.WriteLine(productsArray.GetElement(0));
Console.WriteLine(productsArray.GetElement(1));
Console.WriteLine($"{ productsArray.GetElement(2)}\n");

productsArray.SetElement(new Product { ProductId = 4, ProductImage = "base64image4", ProductName = "Modified boardgame", ProductDescription = "Modified awesome item", ProductPrice = 40m }, 2);

Console.WriteLine($"{productsArray.GetElement(2)}\n");

productsArray.SwapElements(2, 1);

Console.WriteLine(productsArray.GetElement(1));
Console.WriteLine($"{productsArray.GetElement(2)}\n");

Product boardgame1 = new Product { ProductId = 1, ProductImage = "base64image1", ProductName = "Boardgame1", ProductDescription = "First awesome item", ProductPrice = 10m };
Product boardgame2 = new Product { ProductId = 4, ProductImage = "base64image4", ProductName = "Modified boardgame", ProductDescription = "Modified awesome item", ProductPrice = 40m };

Console.WriteLine(productsArray.GetElement(0));
Console.WriteLine(productsArray.GetElement(1));
Console.WriteLine($"{productsArray.GetElement(2)}\n");

//productsArray.SwapElements(boardgame2, boardgame1);

Console.WriteLine(productsArray.GetElement(0));
Console.WriteLine($"{productsArray.GetElement(1)}\n");

//productsArray.SwapElements(boardgame1, 0);

Console.WriteLine(productsArray.GetElement(0));
Console.WriteLine(productsArray.GetElement(1));