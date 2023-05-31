using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NlayerApi.Core.Models;

namespace NlayerApi.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name = "Kalem", CategoryId = 1, CreatedDate = DateTime.Now, Stock = 20, Priace = 10 },
                new Product { Id = 2, Name = "Kitap", CategoryId = 2, CreatedDate = DateTime.Now, Stock = 50, Priace = 100 }
                );
        }
    }
}
