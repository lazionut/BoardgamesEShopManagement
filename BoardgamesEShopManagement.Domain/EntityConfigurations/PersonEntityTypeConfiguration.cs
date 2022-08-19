using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Domain.EntityConfigurations
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> personConfiguration)
        {
            personConfiguration
                .HasOne(person => person.Address)
                .WithOne(address => address.Person)
                .HasForeignKey<Person>(address => address.AddressId);
        }
    }
}