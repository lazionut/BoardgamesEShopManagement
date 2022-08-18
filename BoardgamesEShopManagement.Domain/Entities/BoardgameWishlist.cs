﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class BoardgameWishlist
    {
        public int BoardgameId { get; set; }
        public Boardgame Boardgame { get; set; } = null!;

        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; } = null!;
    }
}