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
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration
                .HasMany(order => order.Boardgames)
                .WithMany(boardgame => boardgame.Orders)
                .UsingEntity<OrderItem>
                (
                wi => wi.HasOne(x => x.Boardgame).WithMany().HasForeignKey(x => x.BoardgameId),
                wi => wi.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId)
                );
        }
    }
}
