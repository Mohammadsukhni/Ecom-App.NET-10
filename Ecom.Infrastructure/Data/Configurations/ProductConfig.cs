using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Price).HasPrecision(18, 2).IsRequired();
            builder.HasOne(x => x.Category).WithMany(x => x.products).HasForeignKey(x => x.CategoryId);
            builder.HasMany(x => x.Photos).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasData(new Product { Id=1, Name="test", Description="test description", Price=10, CategoryId=1 });
        }
    }
}
