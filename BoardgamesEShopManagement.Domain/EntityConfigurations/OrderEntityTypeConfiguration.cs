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
                .HasOne(order => order.Account)
                .WithMany(account => account.Orders)
                .HasForeignKey(order => order.AccountId);

            orderConfiguration
                .Property(order => order.Total)
                .HasPrecision(24, 2);
        }
    }
}
