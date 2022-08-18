using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Domain.EntityConfigurations
{
    public class WishlistEntityTypeConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> wishlistConfiguration)
        {
            wishlistConfiguration
                .HasMany(wishlist => wishlist.Boardgames)
                .WithMany(boardgame => boardgame.Wishlists)
                .UsingEntity<WishlistItem>
                (
                wi => wi.HasOne(x => x.Boardgame).WithMany().HasForeignKey(x => x.BoardgameId),
                wi => wi.HasOne(x => x.Wishlist).WithMany().HasForeignKey(x => x.WishlistId)
                );
        }
    }
}
