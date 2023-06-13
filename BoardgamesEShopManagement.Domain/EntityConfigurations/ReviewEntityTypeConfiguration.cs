using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            reviewConfiguration
                .HasIndex(review => new { review.BoardgameId, review.AccountId })
                .IsUnique();
        }
    }
}