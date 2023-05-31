﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NlayerApi.Core.Models;

namespace NlayerApi.Repository.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x=> x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x=> x.Description).IsRequired().HasMaxLength(300);

            builder.ToTable("Categories");
        }
    }
}
