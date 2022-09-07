using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Domain.EntityConfigurations
{
    public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> addressConfiguration)
        {
            addressConfiguration
                .HasQueryFilter(b => b.IsArchived == false);
        }
    }
}