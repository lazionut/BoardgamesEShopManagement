USE [BoardgamesEShopDB]

INSERT INTO Category (Name)
VALUES 
('Casual'),
('Short'),
('Best for 2'),
('Enthusiast'),
('Cooperative'),
('Wargaming'),
('RPG'),
('Family'),
('Party'),
('Accessories')

INSERT INTO Boardgame (Image, Name, Description, Link, Price, CategoryId)
VALUES
(NULL, 'Splendor', 'In Splendor, you lead a merchant guild. Using tokens representing gemstones, you will acquire developments which produce new gems (bonuses). These bonuses reduce the cost of your purchases and attract noble patrons. Each turn is quick: one, and only one, action! The first player to reach 15 prestige points by accumulating nobles and development cards triggers the end of the game.)', 'https://boardgamegeek.com/boardgame/148228/splendor', 140, 1),
(NULL, 'Azul', 'Introduced by the Moors, azulejos (originally white and blue ceramic tiles) were fully embraced by the Portuguese when their king Manuel I, on a visit to the Alhambra palace in Southern Spain, was mesmerized by the stunning beauty of the Moorish decorative tiles. The king, awestruck by the interior beauty of the Alhambra, immediately ordered that his own palace in Portugal be decorated with similar wall tiles. As a tile-laying artist, you have been challenged to embellish the walls of the Royal Palace of Evora. In the game Azul, players take turns drafting colored tiles from suppliers to their player board. Later in the round, players score points based on how they''ve placed their tiles to decorate the palace. Extra points are scored for specific patterns and completing sets'' wasted supplies harm the player''s score. The player with the most points at the end of the game wins.', 'https://boardgamegeek.com/boardgame/230802/azul', 174, 8),
(NULL, 'Terra Mystica', 'In Terra Mystica each player governs one of 14 factions trying to develop more successfully than their opponents. Terra Mystica is a magical world: its inhabitants are able to transform the terrain they are living in. A faction either lives in the Plains, the Swamp, the Lakes, the Forest, the Mountains, the Wasteland, or the Desert – and each of them strives to transform the terrain according to its needs.', 'https://boardgamegeek.com/boardgame/120677/terra-mystica', 290, 4),
(NULL, 'BANG!', 'BANG! is a game designed by Emiliano Sciarra and published by DV Giochi.', 'https://boardgamegeek.com/boardgame/3955/bang', 99, 9),
(NULL, 'Patchwork', 'Piece together a quilt and leave no holes to become the button master.', 'https://boardgamegeek.com/boardgame/163412/patchwork', 109, 3),
(NULL, 'Pandemic', 'Pandemic is a game designed by Matt Leacock and published by Z-Man Games, Inc.', 'https://boardgamegeek.com/boardgame/30549/pandemic', 155, 5),
(NULL, 'Yahtzee', 'The dice rollin'' battle game: this classic parlor game has captivated players, young and old, since 1956. The unique combination of luck and strategy makes every game an exciting challenge.', 'https://boardgamegeek.com/boardgame/2243/yahtzee', 70, 2),
(NULL, 'The Voyages of Marco Polo', 'In 1271, 17-year-old Marco Polo started on a journey to China with his father and older brother. After a long and grueling journey that led through Jerusalem and Mesopotamia and over the ''Silk Road'', they reached the court of Kublai Khan in 1275. In The Voyages of Marco Polo, players recreate this journey, with each player having a different character and special power in the game.', 'https://boardgamegeek.com/boardgame/171623/voyages-marco-polo', 309, 4),
(NULL, 'Warhammer 40K Collection', 'For the glory.', NULL, 600.5, 4),
(NULL, 'The Voyages of Marco Polo Insert', 'Awesome insert for you componentes!', NULL, 87, 10)

INSERT INTO Review (Title, Author, Content, BoardgameId)
VALUES
('Title1', 'Author1', 'Content1', 1),
('Title2', 'Author2', 'Content2', 2),
('Title3', 'Author3', 'Content3', 3),
('Title4', 'Author4', 'Content4', 4),
('Title5', 'Author5', 'Content5', 5),
('Title6', 'Author6', 'Content6', 6),
('Title7', 'Author7', 'Content7', 7),
('Title8', 'Author8', 'Content8', 8),
('Title9', 'Author9', 'Content9', 9),
('Title10', 'Author10', 'Content10', 10)

INSERT INTO Address (Details, City, County, Country, Phone)
VALUES
('Str. Strada1', 'City1', 'County1', 'Country1', '0712345567'),
('Str. Strada2', 'City2', 'County2', 'Country2', '0712345569'),
('Str. Strada3', 'City3', 'County3', 'Country3', '0712345568'),
('Str. Strada4', 'City4', 'County4', 'Country4', '0712345510'),
('Str. Strada5', 'City5', 'County5', 'Country5', '0712345561'),
('Str. Strada6', 'City6', 'County6', 'Country6', '0712345562'),
('Str. Strada7', 'City7', 'County7', 'Country7', '0712345563'),
('Str. Strada8', 'City8', 'County8', 'Country8', '0712345564'),
('Str. Strada9', 'City9', 'County9', 'Country9', '0712345565'),
('Str. Strada10', 'City10', 'County10', 'Country10', '0712345566')

INSERT INTO Person (FirstName, LastName, Email, AddressId)
VALUES
('FirstName1', 'LastName1', 'email1@gmail.com', 1),
('FirstName2', 'LastName2', 'email2@gmail.com', 2),
('FirstName3', 'LastName3', 'email3@gmail.com', 3),
('FirstName4', 'LastName4', 'email4@gmail.com', 4),
('FirstName5', 'LastName5', 'email5@gmail.com', 5),
('FirstName6', 'LastName6', 'email6@gmail.com', 6),
('FirstName7', 'LastName7', 'email7@gmail.com', 7),
('FirstName8', 'LastName8', 'email8@gmail.com', 8),
('FirstName9', 'LastName9', 'email9@gmail.com', 9),
('FirstName10', 'LastName10', 'email10@gmail.com', 10)

INSERT INTO FinishedOrder (Total, Date)
VALUES
(214, '20220806 00:00:00 AM'),
(140, '20220808 00:00:00 AM'),
(174, '20220809 00:00:00 AM'),
(99, '20220810 00:00:00 AM'),
(155, '20220811 00:00:00 AM'),
(964.5, '20220812 00:00:00 AM'),
(964.5, '20220813 00:00:00 AM'),
(964.5, '20220814 00:00:00 AM'),
(109, '20220817 00:00:00 AM'),
(87, '20220817 00:00:00 AM')

INSERT INTO OrderItem (OrderId, BoardgameId, Quantity)
VALUES
(1, 1, 1),
(1, 2, 1),
(2, 1, 1),
(3, 2, 1),
(4, 6, 1),
(5, 8, 1),
(5, 9, 1),
(6, 8, 1),
(6, 9, 1),
(7, 8, 1),
(7, 9, 1),
(8, 8, 1),
(8, 9, 1),
(9, 5, 1),
(10, 2, 1)

INSERT INTO Wishlist (WishlistName)
VALUES
('MyWishlist1'),
('MyWishlist2'),
('MyWishlist3'),
('MyWishlist4'),
('MyWishlist5'),
('MyWishlist6'),
('MyWishlist7'),
('MyWishlist8'),
('MyWishlist9'),
('MyWishlist10')

INSERT INTO WishlistItem (WishlistId, BoardgameId, Quantity)
VALUES
(1, 1, 1),
(1, 2, 1),
(2, 1, 2),
(3, 2, 1),
(4, 6, 1),
(5, 9, 2),
(6, 9, 1),
(7, 8, 1),
(7, 9, 3),
(7, 8, 1),
(8, 9, 1),
(9, 5, 1),
(10, 2, 1)