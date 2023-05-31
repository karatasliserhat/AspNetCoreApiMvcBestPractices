using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NlayerApi.Core.Models;

namespace NlayerApi.Repository.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Priace).IsRequired().HasColumnType("decimal(18,2)");

            builder.ToTable("Products");
            builder.HasOne(x=> x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
        }
    }
}
