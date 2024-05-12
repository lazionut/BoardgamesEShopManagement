using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasIndex(account => account.Email)
                .IsUnique();

            accountConfiguration
                .HasQueryFilter(b => !b.IsArchived);
        }
    }
}