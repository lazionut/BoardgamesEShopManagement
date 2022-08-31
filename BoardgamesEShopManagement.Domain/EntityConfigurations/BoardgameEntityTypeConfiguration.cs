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
    public class BoardgameEntityTypeConfiguration : IEntityTypeConfiguration<Boardgame>
    {
        public void Configure(EntityTypeBuilder<Boardgame> boardgameConfiguration)
        {
            boardgameConfiguration
                .HasOne(boardgame => boardgame.Category)
                .WithMany(category => category.Boardgames);

            boardgameConfiguration
                .Property(order => order.Price)
                .HasPrecision(9, 2);

            boardgameConfiguration
                .HasQueryFilter(b => b.IsArchived == false);
        }
    }
}
