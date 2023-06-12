using BoardgamesEShopManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardgamesEShopManagement.Domain.EntityConfigurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> categoryConfiguration)
        {
            categoryConfiguration
                .HasMany(category => category.Boardgames)
                .WithOne(boardgame => boardgame.Category)
                .HasForeignKey(boardgame => boardgame.CategoryId);
        }
    }
}