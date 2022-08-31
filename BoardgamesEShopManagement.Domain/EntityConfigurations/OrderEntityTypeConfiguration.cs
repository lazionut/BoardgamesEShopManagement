using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BoardgamesEShopManagement.Domain.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration
                .HasMany(order => order.Boardgames)
                .WithMany(boardgame => boardgame.Orders)
                .UsingEntity<OrderItem>
                (
                oi => oi.HasOne(oi => oi.Boardgame).WithMany().HasForeignKey(oi => oi.BoardgameId),
                oi => oi.HasOne(oi => oi.Order).WithMany().HasForeignKey(oi => oi.OrderId)
                );

            orderConfiguration
                .HasOne(order => order.Account)
                .WithMany(account => account.Orders)
                .HasForeignKey(order => order.AccountId);

            orderConfiguration
                .Property(order => order.Total)
                .HasPrecision(18, 2);
        }
    }
}
