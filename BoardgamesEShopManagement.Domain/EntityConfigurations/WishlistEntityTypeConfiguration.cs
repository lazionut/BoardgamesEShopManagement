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
                wi => wi.HasOne(wi => wi.Boardgame).WithMany().HasForeignKey(wi => wi.BoardgameId),
                wi => wi.HasOne(wi => wi.Wishlist).WithMany().HasForeignKey(wi => wi.WishlistId)
                );

            wishlistConfiguration
                .HasOne(wishlist => wishlist.Account)
                .WithMany(account => account.Wishlist)
                .HasForeignKey(order => order.AccountId);
        }
    }
}