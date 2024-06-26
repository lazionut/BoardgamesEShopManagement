﻿using BoardgamesEShopManagement.Domain.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Infrastructure
{
    public static class Seeder
    {
        public static void SeedData()
        {
            using ShopContext context = new ShopContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            List<Domain.Entities.Address> addresses = GetPreconfiguredAddress().ToList();
            List<Account> accounts = GetPreconfiguredAccount().ToList();
            List<Category> categories = GetPreconfiguredCategories().ToList();
            List<Boardgame> boardgames = GetPreconfiguredBoardgames().ToList();
            List<Review> reviews = GetPreconfiguredReviews().ToList();
            List<Wishlist> wishlists = GetPreconfiguredWishlists().ToList();
            List<WishlistItem> wishlistItems = GetPreconfiguredWishlistItems().ToList();
            List<Order> orders = GetPreconfiguredOrders().ToList();
            List<OrderItem> orderItems = GetPreconfiguredOrderItems().ToList();

            context.Addresses.AddRange(addresses);

            context.SaveChanges();

            context.Accounts.AddRange(accounts);
            context.Categories.AddRange(categories);

            context.SaveChanges();

            context.Boardgames.AddRange(boardgames);

            context.SaveChanges();

            context.Reviews.AddRange(reviews);
            context.Wishlists.AddRange(wishlists);
            context.Orders.AddRange(orders);

            context.SaveChanges();

            context.WishlistItems.AddRange(wishlistItems);
            context.OrderItems.AddRange(orderItems);

            context.Database.ExecuteSqlRaw("alter authorization on database::BoardgamesEShopDBDev to sa");

            context.SaveChanges();
        }

        private static IEnumerable<Domain.Entities.Address> GetPreconfiguredAddress()
        {
            List<string> phoneNumbers = Enumerable.Range(1, 10)
                .Select(_ => new Faker().Phone.PhoneNumber())
                .ToList();

            return phoneNumbers.Select(phoneNumber =>
                new Faker<Domain.Entities.Address>()
                   .RuleFor(address => address.Details, faker => faker.Address.StreetAddress())
                   .RuleFor(address => address.City, faker => faker.Address.City())
                   .RuleFor(address => address.County, faker => faker.Address.County())
                   .RuleFor(address => address.Country, faker => faker.Address.Country())
                   .RuleFor(address => address.Phone, phoneNumber)
                   .Generate());
        }

        private static IEnumerable<Account> GetPreconfiguredAccount()
        {
            List<int> addressIds = Enumerable.Range(1, 10).ToList();

            return addressIds.Select(addressId =>
               new Faker<Account>()
                .RuleFor(account => account.FirstName, faker => faker.Person.FirstName)
                .RuleFor(account => account.LastName, faker => faker.Person.LastName)
                .RuleFor(account => account.Email, faker => faker.Internet.Email())
                .RuleFor(account => account.PasswordHash, faker => faker.Random.Hash())
                .RuleFor(account => account.AddressId, addressId)
                .Generate());
        }

        private static IEnumerable<Category> GetPreconfiguredCategories() =>
            new List<Category>
            {
            new Category
            {
                Name = "Casual"
            },
            new Category
            {
                Name = "Short"
            },
            new Category
            {
                Name = "Best for 2"
            },
            new Category
            {
                Name = "Enthusiast"
            },
            new Category
            {
                Name = "Cooperative"
            },
            new Category
            {
                Name = "Wargaming"
            },
            new Category
            {
                Name = "RPG"
            },
            new Category
            {
                Name = "Family"
            },
            new Category
            {
                Name = "Party"
            },
            new Category
            {
                Name = "Accessories"
            },
            };

        private static IEnumerable<Boardgame> GetPreconfiguredBoardgames()
        {
            string[] boardgameNames =
            {
            "Splendor",
            "Azul",
            "Terra Mystica",
            "BANG!",
            "Patchwork",
            "Pandemic",
            "Yahtzee",
            "The Voyages of Marco Polo",
            "Warhammer 40K Collection",
            "The Voyages of Marco Polo Insert",
            };

            string?[] boardgameLinks =
            {
            "https://boardgamegeek.com/boardgame/148228/splendor",
            "https://boardgamegeek.com/boardgame/230802/azul",
            "https://boardgamegeek.com/boardgame/120677/terra-mystica",
            "https://boardgamegeek.com/boardgame/3955/bang",
            "https://boardgamegeek.com/boardgame/163412/patchwork",
            "https://boardgamegeek.com/boardgame/30549/pandemic",
            "https://boardgamegeek.com/boardgame/2243/yahtzee",
            "https://boardgamegeek.com/boardgame/171623/voyages-marco-polo",
            null,
            null,
            };

            return boardgameNames.Select(boardgameName =>
                new Faker<Boardgame>()
                    .RuleFor(boardgame => boardgame.Name, boardgameName)
                    .RuleFor(boardgame => boardgame.ReleaseYear, faker => faker.Random.Int(2000, 2020))
                    .RuleFor(boardgame => boardgame.Description, (_, boardgame) => boardgame.Name.ToUpper())
                    .RuleFor(boardgame => boardgame.Link, faker => faker.PickRandom(boardgameLinks))
                    .RuleFor(boardgame => boardgame.Price, faker => faker.Random.Decimal(50, 1500))
                    .RuleFor(boardgame => boardgame.Quantity, faker => faker.Random.Int(1, 100))
                    .RuleFor(boardgame => boardgame.CategoryId, faker => faker.Random.Number(1, 10))
                    .Generate());
        }

        private static IEnumerable<Review> GetPreconfiguredReviews()
        {
            List<int> reviewAccountIds = Enumerable.Range(1, 10).ToList();

            return reviewAccountIds.Select(reviewAccountId =>
                new Faker<Review>()
                    .RuleFor(review => review.Title, faker => faker.Rant.Review().Substring(0, faker.Rant.Review().IndexOf(' ')))
                    .RuleFor(book => book.Author, faker => faker.Person.FullName)
                    .RuleFor(review => review.Score, faker => faker.Random.Byte(1, 5))
                    .RuleFor(review => review.Content, faker => faker.Rant.Review())
                    .RuleFor(review => review.BoardgameId, reviewAccountId)
                    .RuleFor(review => review.AccountId, faker => faker.Random.Int(1, 10))
                    .Generate());
        }

        private static IEnumerable<Wishlist> GetPreconfiguredWishlists()
        {
            string[] wishlistNames =
            {
            "Wishlist1",
            "Wishlist2",
            "Wishlist3",
            "Wishlist4",
            "Wishlist5",
            "Wishlist6",
            "Wishlist7",
            "Wishlist8",
            "Wishlist9",
            "Wishlist10",
            };

            return wishlistNames.Select(wishlistName =>
                new Faker<Wishlist>()
                    .RuleFor(wishlist => wishlist.Name, wishlistName)
                    .RuleFor(wishlist => wishlist.AccountId, faker => faker.Random.Number(1, 10))
                    .Generate());
        }

        private static IEnumerable<WishlistItem> GetPreconfiguredWishlistItems()
        {
            List<int> wishlistIds = Enumerable.Range(1, 10)
                //.Select(_ => new Faker().Random.Number(1, 10))
                .ToList();

            return wishlistIds.Select(wishlistId =>
                new Faker<WishlistItem>()
                    .RuleFor(wishlistItem => wishlistItem.WishlistId, wishlistId)
                    .RuleFor(wishlistItem => wishlistItem.BoardgameId, faker => faker.Random.Number(1, 10))
                    .Generate());
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            List<decimal> orderTotals = Enumerable.Range(1, 10)
                 .Select(_ => new Faker().Random.Decimal(50, 10000))
                 .ToList();

            return orderTotals.Select(orderTotal =>
                new Faker<Order>()
                .RuleFor(order => order.FullName, faker => faker.Person.FullName)
                    .RuleFor(order => order.Address, faker =>
                    faker.Address.StreetAddress() + ' ' +
                    faker.Address.City() + ' ' + faker.Address.County() + ' ' +
                    faker.Address.Country() + ' ' + faker.Phone.PhoneNumber())
                    .RuleFor(order => order.Total, orderTotal)
                    .RuleFor(order => order.AccountId, faker => faker.Random.Number(1, 10))
                    .Generate());
        }

        private static IEnumerable<OrderItem> GetPreconfiguredOrderItems()
        {
            List<int> orderIds = Enumerable.Range(1, 10)
                //.Select(_ => new Faker().Random.Number(1, 10))
                .ToList();

            return orderIds.Select(orderId =>
                new Faker<OrderItem>()
                    .RuleFor(orderItem => orderItem.OrderId, orderId)
                    .RuleFor(orderItem => orderItem.BoardgameId, faker => faker.Random.Number(1, 10))
                    .RuleFor(orderItem => orderItem.Quantity, faker => faker.Random.Number(1, 3))
                    .RuleFor(orderItem => orderItem.Price, faker => faker.Random.Decimal(50, 10000))
                    .Generate());
        }
    }
}