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
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> reviewConfiguration)
        {
            reviewConfiguration
                .HasOne(review => review.Boardgame)
                .WithMany(boardgame => boardgame.Reviews)
                .HasForeignKey(review => review.BoardgameId);

            reviewConfiguration
                .HasOne(review => review.Account)
                .WithMany(account => account.Review)
                .HasForeignKey(review => review.AccountId);
        }
    }
}