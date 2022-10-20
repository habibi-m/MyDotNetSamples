using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ParentChildComponent.Models;

namespace ParentChildComponent.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, CategoryId = 1, Name = "product1", Price = 100 },
                new Product { Id = 2, CategoryId = 1, Name = "product2", Price = 200 },
                new Product { Id = 3, CategoryId = 3, Name = "product3", Price = 300 },
                new Product { Id = 4, CategoryId = 4, Name = "product4", Price = 400 },
                new Product { Id = 5, CategoryId = 8, Name = "product5", Price = 500 },
                new Product { Id = 6, CategoryId = 8, Name = "product6", Price = 600 },
                new Product { Id = 7, CategoryId = 10, Name = "product7", Price = 700 }
            );
        }
    }
}
