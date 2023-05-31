using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NlayerApi.Core.Models;

namespace NlayerApi.Repository.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Kalemler", Description = "Kalemler Category", CreatedDate = DateTime.Now },
           new Category { Id = 2, Name = "Kitaplar", Description = "Kitaplar Category", CreatedDate = DateTime.Now }
                );
        }
    }
}
