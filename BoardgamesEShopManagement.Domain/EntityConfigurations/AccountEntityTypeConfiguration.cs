using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Domain.EntityConfigurations
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> accountConfiguration)
        {
            accountConfiguration
                .HasOne(account => account.Address)
                .WithOne(address => address.Account)
                .HasForeignKey<Account>(address => address.AddressId);

            accountConfiguration
                .HasQueryFilter(b => b.IsArchived == false);
        }
    }
}