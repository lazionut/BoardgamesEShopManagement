--wishlisted boardgames
SELECT Boardgame.Name
FROM Boardgame
INNER JOIN WishlistItem ON Boardgame.Id = WishlistItem.BoardgameId;

--boardgames that appear in more than one order
SELECT Boardgame.Name, COUNT(OrderItem.BoardgameId) AS 'OrderedBoardgames'
FROM Boardgame
INNER JOIN OrderItem ON OrderItem.BoardgameId = Boardgame.Id
GROUP BY Boardgame.Name
HAVING COUNT(OrderItem.BoardgameId) > 1;

--categories descending sorted by name
SELECT Category.Name
FROM Category
ORDER BY Category.Name DESC;

--money earned by each sold boardgame
SELECT Boardgame.Name, SUM(Boardgame.Price) AS 'EarnedMoney'
FROM Boardgame
INNER JOIN OrderItem ON OrderItem.BoardgameId = Boardgame.Id
GROUP BY Boardgame.Name, Boardgame.Price
HAVING Boardgame.Price > 0;

--wishlisted and ordered boardgames
SELECT DISTINCT Boardgame.Name
FROM Boardgame
INNER JOIN WishlistItem ON Boardgame.Id = WishlistItem.BoardgameId
INNER JOIN OrderItem ON Boardgame.Id = OrderItem.BoardgameId;

--average price of boardgames without an available link
SELECT Boardgame.Name, AVG(Boardgame.Price) AS 'AverageBoardgamePrice'
FROM Boardgame
WHERE Boardgame.Link IS NULL
GROUP BY Boardgame.Name, Boardgame.Price
HAVING Boardgame.Price > 0;

--boardgames with description length of minimum 500 characters which were wishlishted more than once
SELECT Boardgame.Name, COUNT(WishlistItem.BoardgameId) AS 'WishlistedTimes'
FROM Boardgame
INNER JOIN WishlistItem ON Boardgame.Id = WishlistItem.BoardgameId
WHERE LEN(Boardgame.Description) >= 500
GROUP BY Boardgame.Name, Boardgame.Price, WishlistItem.BoardgameId
HAVING Boardgame.Price > 1;

--ascending sorted boardgames by their name which were wishlisted more than once in the same wishlist
SELECT Boardgame.Name, WishlistItem.Quantity
FROM Boardgame
INNER JOIN WishlistItem ON Boardgame.Id = WishlistItem.BoardgameId
WHERE WishlistItem.Quantity > 1
ORDER BY Boardgame.Name;

--boardgames that are more expensive than the average price
SELECT Boardgame.Name, Boardgame.Price
FROM Boardgame
WHERE Boardgame.Price > (
    SELECT AVG(Boardgame.Price)
    FROM Boardgame
);

--boardgames wishlishted less than the average of the number of wishlishted boardgames descending sorted by how many times were wishlishted
SELECT Boardgame.Name, COUNT(WishlistItem.BoardgameId) AS 'TimesWishlishted'
FROM Boardgame
INNER JOIN WishlistItem ON Boardgame.Id = WishlistItem.BoardgameId
GROUP BY Boardgame.Name, Boardgame.Id
HAVING COUNT(WishlistItem.BoardgameId) < (
    SELECT AVG(WishlistItem.BoardgameId)
	FROM Boardgame
	INNER JOIN WishlistItem ON Boardgame.Id = WishlistItem.BoardgameId
)
ORDER BY COUNT(WishlistItem.BoardgameId) DESC;