using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Infrastructure.Repositories;

namespace BoardgamesEShopManagement.Test
{
    public class WishlistRepositoryTests
    {
        [Fact]
        public void AddWishlistToWishlistList()
        {
            Boardgame newBoardgame = new Boardgame { CategoryId = 1, Image = "base64image", Name = "NewBoardgame", Description = "New boardgame description", Price = 30m };
            WishlistItem wishlistItem = new WishlistItem { Boardgame = newBoardgame, Quantity = 1 };
            List<WishlistItem> boardgamesWishlistItemList = new();
            boardgamesWishlistItemList.Add(wishlistItem);
            Wishlist newWishlist = new Wishlist { Name = "MyWishlist" };

            WishlistRepository wishlistRepository = new();
            wishlistRepository.CreateWishlist(newWishlist);

            Assert.True(wishlistRepository.wishlists.Count == 1);
            Assert.Contains<Wishlist>(newWishlist, wishlistRepository.wishlists);
        }

        [Fact]
        public void GetWishlistByIdFromWishlistsList()
        {
            Boardgame newBoardgame = new Boardgame { CategoryId = 1, Image = "base64image", Name = "NewBoardgame", Description = "New boardgame description", Price = 30m };
            WishlistItem wishlistItem = new WishlistItem { Boardgame = newBoardgame, Quantity = 1 };
            List<WishlistItem> boardgamesWishlistItemList = new();
            boardgamesWishlistItemList.Add(wishlistItem);
            Wishlist newWishlist = new Wishlist { Name = "MyWishlist" };

            WishlistRepository wishlistRepository = new();
            wishlistRepository.CreateWishlist(newWishlist);
            Wishlist firstWishlist = wishlistRepository.GetWishlist(1);

            newWishlist.Should().BeSameAs(firstWishlist);
        }


        [Fact]
        public void GetWishlistsList()
        {
            Boardgame firstNewBoardgame = new Boardgame { CategoryId = 1, Image = "base64image1", Name = "NewBoardgame1", Description = "New boardgame1 description", Price = 40m };
            Boardgame secondNewBoardgame = new Boardgame { CategoryId = 2, Image = "base64image2", Name = "NewBoardgame2", Description = "New boardgame2 description", Price = 43m };
            Boardgame thirdNewBoardgame = new Boardgame { CategoryId = 1, Image = "base64image3", Name = "NewBoardgame3", Description = "New boardgame3 description", Price = 37m };
            WishlistItem firstWishlistItem = new WishlistItem { Boardgame = firstNewBoardgame, Quantity = 2 };
            WishlistItem secondWishlistItem = new WishlistItem { Boardgame = secondNewBoardgame, Quantity = 1 };
            WishlistItem thirdWishlistItem = new WishlistItem { Boardgame = thirdNewBoardgame, Quantity = 3 };
            List<WishlistItem> boardgamesWishlistItemList = new();
            boardgamesWishlistItemList.Add(firstWishlistItem);
            boardgamesWishlistItemList.Add(secondWishlistItem);
            boardgamesWishlistItemList.Add(thirdWishlistItem);
            Wishlist firstNewWishlist = new Wishlist { Name = "MyWishlist" };
            Wishlist secondNewWishlist = new Wishlist { Name = "MySameWishlist" };

            WishlistRepository wishlistRepository = new();
            wishlistRepository.CreateWishlist(firstNewWishlist);
            wishlistRepository.CreateWishlist(secondNewWishlist);
            List<Wishlist> wishlists = wishlistRepository.GetWishlists().ToList();

            wishlists.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.ContainInOrder(new[] { firstNewWishlist, secondNewWishlist })
                .And.ContainItemsAssignableTo<Wishlist>();
        }

        [Fact]
        public void DeleteWishlistFromWishlistsList()
        {
            Boardgame newBoardgame = new Boardgame { CategoryId = 3, Image = "base64image", Name = "NewBoardgame", Description = "New boardgame description", Price = 30m };
            WishlistItem wishlistItem = new WishlistItem { Boardgame = newBoardgame, Quantity = 1 };
            List<WishlistItem> boardgamesWishlistItemList = new();
            boardgamesWishlistItemList.Add(wishlistItem);
            Wishlist newWishlist = new Wishlist { Name = "MyWishlist" };

            WishlistRepository wishlistRepository = new();
            wishlistRepository.DeleteWishlist(newWishlist.Id);

            Assert.True(wishlistRepository.wishlists.Count == 0);
        }
    }
}
