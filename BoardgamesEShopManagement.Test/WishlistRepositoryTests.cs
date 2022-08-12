using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Infrastructure.Repositories;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Test
{
    public class WishlistRepositoryTests
    {
        [Fact]
        public void AddWishlistToWishlistList()
        {
            Boardgame newBoardgame = new Boardgame { BoardgameImage = "base64image", BoardgameName = "NewBoardgame", BoardgameDescription = "New boardgame description", BoardgamePrice = 30m };
            WishlistItem wishlistItem = new WishlistItem { Boardgame = newBoardgame, Quantity = 1 };
            List<WishlistItem> boardgamesWishlistItemList = new();
            boardgamesWishlistItemList.Add(wishlistItem);
            Wishlist newWishlist = new Wishlist { WishlistName = "MyWishlist", WishlistBoardgames = new List<WishlistItem>(boardgamesWishlistItemList) };

            WishlistRepository wishlistRepository = new();
            wishlistRepository.CreateWishlist(newWishlist);

            Assert.True(wishlistRepository.wishlists.Count == 1);
            Assert.Contains<Wishlist>(newWishlist, wishlistRepository.wishlists);
        }

        [Fact]
        public void GetWishlistByIdFromWishlistsList()
        {
            Boardgame newBoardgame = new Boardgame { BoardgameImage = "base64image", BoardgameName = "NewBoardgame", BoardgameDescription = "New boardgame description", BoardgamePrice = 30m };
            WishlistItem wishlistItem = new WishlistItem { Boardgame = newBoardgame, Quantity = 1 };
            List<WishlistItem> boardgamesWishlistItemList = new();
            boardgamesWishlistItemList.Add(wishlistItem);
            Wishlist newWishlist = new Wishlist { WishlistName = "MyWishlist", WishlistBoardgames = new List<WishlistItem>(boardgamesWishlistItemList) };

            WishlistRepository wishlistRepository = new();
            wishlistRepository.CreateWishlist(newWishlist);
            Wishlist firstWishlist = wishlistRepository.GetById(1);

            newWishlist.Should().BeSameAs(firstWishlist);
        }


        [Fact]
        public void GetWishlistsList()
        {
            Boardgame firstNewBoardgame = new Boardgame { BoardgameImage = "base64image1", BoardgameName = "NewBoardgame1", BoardgameDescription = "New boardgame1 description", BoardgamePrice = 40m };
            Boardgame secondNewBoardgame = new Boardgame { BoardgameImage = "base64image2", BoardgameName = "NewBoardgame2", BoardgameDescription = "New boardgame2 description", BoardgamePrice = 43m };
            Boardgame thirdNewBoardgame = new Boardgame { BoardgameImage = "base64image3", BoardgameName = "NewBoardgame3", BoardgameDescription = "New boardgame3 description", BoardgamePrice = 37m };
            WishlistItem firstWishlistItem = new WishlistItem { Boardgame = firstNewBoardgame, Quantity = 2 };
            WishlistItem secondWishlistItem = new WishlistItem { Boardgame = secondNewBoardgame, Quantity = 1 };
            WishlistItem thirdWishlistItem = new WishlistItem { Boardgame = thirdNewBoardgame, Quantity = 3 };
            List<WishlistItem> boardgamesWishlistItemList = new();
            boardgamesWishlistItemList.Add(firstWishlistItem);
            boardgamesWishlistItemList.Add(secondWishlistItem);
            boardgamesWishlistItemList.Add(thirdWishlistItem);
            Wishlist firstNewWishlist = new Wishlist { WishlistName = "MyWishlist", WishlistBoardgames = new List<WishlistItem>(boardgamesWishlistItemList) };
            Wishlist secondNewWishlist = new Wishlist { WishlistName = "MySameWishlist", WishlistBoardgames = new List<WishlistItem>(boardgamesWishlistItemList) };

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
        public void DeleteWishlistFromWishlistLists()
        {
            Boardgame newBoardgame = new Boardgame { BoardgameImage = "base64image", BoardgameName = "NewBoardgame", BoardgameDescription = "New boardgame description", BoardgamePrice = 30m };
            WishlistItem wishlistItem = new WishlistItem { Boardgame = newBoardgame, Quantity = 1 };
            List<WishlistItem> boardgamesWishlistItemList = new();
            boardgamesWishlistItemList.Add(wishlistItem);
            Wishlist newWishlist = new Wishlist { WishlistName = "MyWishlist", WishlistBoardgames = new List<WishlistItem>(boardgamesWishlistItemList) };

            WishlistRepository wishlistRepository = new();
            wishlistRepository.DeleteWishlist(newWishlist.Id);

            Assert.True(wishlistRepository.wishlists.Count == 0);
        }
    }
}
