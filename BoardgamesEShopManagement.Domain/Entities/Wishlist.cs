﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class Wishlist : EntityBase
    {
        [MaxLength(50)]
        public string WishlistName { get; set; } = null!;
        public ICollection<Boardgame> Boardgames { get; set; } = null!;
    }
}
